using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;


namespace Utils
{
    /// <summary>
    /// Created by Jay Van Schaick. This class serves as a way to handle multi-threading much like a simplistic version of the ThreadPool Class, however with a little more functionality towards game design.
    /// Specifically meant to work with the Unity game-engine. 
    /// </summary>
    public static class MultithreadingJobsHandler
    {

        public static ErrorCall ErrorReport;

        // Multi-threading Jobs System Variables

        // A list to hold jobs that are queue until a thread calls them.
        static List<MultithreadingJob> _jobListQueue = new List<MultithreadingJob>();

        //This is so that the threads can be numbered. If a boolean is true it is in use, otherwise it's false. Threads use the index position as their number.
        static bool[] _threadNumbers;

        //A lock to control access to the JobListQueue.First() 
        static readonly object _getJobLock = new object();



        /// <summary>
        /// The current amount of threads created and still in use.
        /// </summary>
        static int CurrentThreadsAmount
        {
            get
            {
                int counter = 0;

                for (int i = 0; i < _threadNumbers.Length; i++)
                {
                    if (_threadNumbers[i] == true)
                    {
                        counter++;
                    }
                }

                return counter;
            }
        }

        /// <summary>
        /// The max amount of threads this Multi-threading Jobs Handler will have running at one time.
        /// </summary>
        public static int GetMaxCurrentThreads
        {
            get
            {
                int ThreadCount = System.Environment.ProcessorCount;

                // Minus one tread, because the main tread will not be used.
                ThreadCount--;

                // If the amount of threads a system have is more than 4, no need to use all of them.  
                if (ThreadCount > 4)
                {
                    ThreadCount--;
                }
                return ThreadCount;
            }
        }

        /// <summary>
        /// Constructor, do set up.
        /// </summary>
        static MultithreadingJobsHandler()
        {

            _threadNumbers = new bool[GetMaxCurrentThreads];

        }

        /// <summary>
        /// Must be called from the main thread once every loop for this class to work. ( EX. In Unity3D, a MonoBehaviour derived class every update.) 
        /// </summary>
        public static void Update()
        {
            CheckToMakeNewThreads();

        }

        /// <summary>
        /// Added a job to be queued for when a thread is next free.
        /// <example>
        /// <para/>An example of using this should be: 
        /// MultithreadingJobsHandler.AddJob(new Func &lt; int threadNumber, param1, param2, param3, etc., out param &gt;(Some Method), new Object[]{param1, param2, param3, etc.}, new Action &lt; param &gt;(Some Return Method) );
        /// </example>
        /// </summary>
        /// <param name="Func_MethodForOtherThread">The Function to call on an other tread. NOTE: This function's first Parameter must be an int which the MultithreadingJobsHandler will add a thread number (or current job number), 
        /// to be used inside of the function to separate it from the same function running on a different thread (job).</param>
        /// <param name="MethodForOtherThreadParameters">The parameters of the function being called on the other tread. NOTE: The MultithreadingJobsHandler will add an int to the front of this.</param>
        /// <param name="Action_ReturnDelegate">The function to call when the first thread returns its calculations (NOTE: the input parameters must be the same as the return parameters of FuncOtherTread).</param>
        /// 
        public static void AddJobWithReturn(object Func_MethodForOtherThread, object[] MethodForOtherThreadParameters, object Action_ReturnDelegate)
        {

            string error;

            try
            {
                if (Func_MethodForOtherThread is Delegate && Action_ReturnDelegate is Delegate)
                {

                    if (FunctionSignatureMatch((Delegate)Func_MethodForOtherThread, MethodForOtherThreadParameters, out error))
                    {

                        Delegate FuncOtherTreadDelegate = (Delegate)Func_MethodForOtherThread;
                        Delegate returnDelegateDelegate = (Delegate)Action_ReturnDelegate;

                        ParameterInfo[] Parameters = returnDelegateDelegate.Method.GetParameters();

                        if (Parameters.Length == 1 && Parameters[0].ParameterType == FuncOtherTreadDelegate.Method.ReturnParameter.ParameterType)
                        {

                            MultithreadingJobWithReturn multithreadingJob = new MultithreadingJobWithReturn(Func_MethodForOtherThread, MethodForOtherThreadParameters, Action_ReturnDelegate);
                            _jobListQueue.Add(multithreadingJob);

                        }
                        else
                        {
                            throw new Exception("The FuncOtherTread return value did not match the returnDelegate's parameter signature");
                        }

                    }
                    else
                    {
                        throw new Exception("The Function's Signature did not match the inputed Arguments: " + error);
                    }
                }
                else
                {
                    throw new Exception("Both the FuncOtherTread and the returnDelegate need to be a Delegate");
                }
            }
            catch (Exception ex)
            {

                HandleError(ex);

            }





        }

        /// <summary>
        /// Added a job to be queued for when a thread is next free. The Delegate used must have a return type of void.
        /// <example>
        /// <para/>An example of using this should be: 
        /// MultithreadingJobsHandler.AddJob(new Action &lt; int threadNumber, param1, param2, param3, etc. &gt;(Some Method), new Object[]{param1, param2, param3, etc.});
        /// </example>
        /// </summary>
        /// <param name="Action_MethodForOtherThread">The Method in the form of a Action to call on an other tread. NOTE: This function's first Parameter must be an int which the MultithreadingJobsHandler will add a thread number (or current job number), 
        /// to be used inside of the function to separate it from the same function running on a different thread (job).</param>
        /// <param name="MethodForOtherThreadParameters">The parameters of the function being called on the other tread. NOTE: The MultithreadingJobsHandler will add an int to the first of this.</param>
        public static void AddJob(object Action_MethodForOtherThread, object[] MethodForOtherThreadParameters)
        {

            string error;

            try
            {
                if (Action_MethodForOtherThread is Delegate)
                {

                    if (FunctionSignatureMatch((Delegate)Action_MethodForOtherThread, MethodForOtherThreadParameters, out error))
                    {
                        MultithreadingJob multithreadingJob = new MultithreadingJob(Action_MethodForOtherThread, MethodForOtherThreadParameters);
                        _jobListQueue.Add(multithreadingJob);
                    }
                    else
                    {
                        throw new Exception("The Function's Signature did not match the inputed Arguments: " + error);
                    }

                }
                else
                {
                    throw new Exception("The FuncOtherTread parameter needs to be a Delegate");
                }
            }
            catch (Exception ex)
            {

                HandleError(ex);

            }


        }

        /// <summary>
        /// Handles the running of a child thread, by getting new jobs after old one are completed. 
        /// </summary>
        /// <param name="ThreadNumber">The number assigned to the thread and passed to the functions off loaded on to the thread.</param>
        static void RunOtherThread(int ThreadNumber)
        {
            MultithreadingJob job;

            do
            {

                try
                {
                    //Get the first job in JobListQue. Put in lock to make sure a race condition was not created between threads
                    lock (_getJobLock)
                    {

                        job = _jobListQueue.First();
                        _jobListQueue.Remove(job);
                    }

                    if (job != null)
                    {

                        job.AddJobNumber(ThreadNumber);

                        job.MultithreadingJobHandler();

                    }

                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }

            } while (_jobListQueue != null && _jobListQueue.Count != 0);

            UnReserveThreadNumber(ThreadNumber);

        }

        /// <summary>
        /// Checks to see if a method's signature matches an array of objects;
        /// </summary>
        /// <param name="MethodToCheck">The method who's signature needs to be checked.</param>
        /// <param name="MethodToCheckParameters">The objects to match against.</param>
        /// <param name="Error">A string to be outputted if an error occurs.</param>
        /// <returns>Returns a boolean, true if matches, false if not.</returns>
        static bool FunctionSignatureMatch(Delegate MethodToCheck, object[] MethodToCheckParameters, out string Error)
        {

            ParameterInfo[] Parameters = MethodToCheck.Method.GetParameters();

            // If function does not a match with parameter length plus one, than there is no match. Return false! 
            // It must have one more parameter because the thread number is going to be pass into the function later on.
            if ((MethodToCheckParameters.Length + 1 != Parameters.Length))
            {
                Error = "The " + MethodToCheck.Method.Name + " does not have the same amount of arguments as was inputed";

                if ((MethodToCheckParameters.Length == Parameters.Length))
                    Error += " (Possible Cause : The First Parameter of " + MethodToCheck.Method.Name + " Must Take an Int for the Multi-threading Job System to input A thread number)";
                return false;
            }

            for (int i = 0; i < Parameters.Length; i++)
            {
                // The first parameter must be an int for the multi-threading system to give a thread number to the method.
                if (i == 0 && Parameters[0].ParameterType != typeof(int))
                {
                    Error = "The First Parameter of " + MethodToCheck.Method.Name + " Must Take an Int for the Multi-threading Job System to input a thread number";
                    return false;
                }
                // For every value after the first parameter see if they check if so do nothing
                else if (i != 0 && Parameters[i].ParameterType != MethodToCheckParameters[i - 1].GetType())
                {

                    // See if it can be converted, if not signature does not match. Return false!
                    if (CanBeCastAsTypeWanted(Parameters[i].ParameterType, MethodToCheckParameters[i - 1], out object temp) == false)
                    {
                        Error = MethodToCheck.Method.Name + " Argument " + i.ToString() + " which is of Type " + Parameters[i].ToString() +
                                " does not match the inputed argument which is of type " + MethodToCheckParameters[i - 1].GetType().ToString();
                        return false;
                    }
                    else // If converted use new type.
                    {
                        MethodToCheckParameters[i - 1] = temp;
                    }

                }


            }
            Error = "";
            return true;
        }

        /// <summary>
        /// Checks to see if a type can be converted, if so does the conversion. 
        /// </summary>
        /// <param name="wanted">The wanted type to be convert to.</param>
        /// <param name="current">The current type.</param>
        /// <param name="ConvertedObject">If the conversion was successful, output the new converted type.</param>
        /// <returns>Returns a boolean, true if type was converted, false if otherwise.</returns>
        static bool CanBeCastAsTypeWanted(Type wanted, object current, out object ConvertedObject)
        {
            try
            {
                ConvertedObject = Convert.ChangeType(current, wanted);
                return true;
            }
            catch (Exception)
            {
                ConvertedObject = null;
                return false;
            }

        }

        /// <summary>
        /// Check to see if new threads are need and if so makes them.
        /// </summary>
        static void CheckToMakeNewThreads()
        {
            // A counter to represent jobs assigned to new thread in last iteration of the while loop to make sure that more threads are not created than have possible jobs.
            int jobsAssignedCounter = 0;

            while (CurrentThreadsAmount < GetMaxCurrentThreads && _jobListQueue.Count > 0 + jobsAssignedCounter)
            {

                // An out number if a thread number it available.
                int reservedThreadNumber;

                if (ReserveThreadNumber(out reservedThreadNumber))
                {

                    // Create and start the new job.
                    // Used AboveNormal thread priority to create less amount of delay for users.
                    Thread jobThread = new Thread(() => RunOtherThread(reservedThreadNumber));
                    jobThread.Priority = System.Threading.ThreadPriority.AboveNormal;
                    jobThread.Start();

                }

                jobsAssignedCounter++;

            }

        }

        /// <summary>
        /// Tries to reserve a new thread number.
        /// </summary>
        /// <param name="ReserveNumber">If a thread number is found, outputs the number.</param>
        /// <returns>Returns a boolean, true if a number is found, false if not.</returns>
        static bool ReserveThreadNumber(out int ReserveNumber)
        {

            for (int i = 0; i < _threadNumbers.Length; i++)
            {
                if (_threadNumbers[i] == false) //is job number available?
                {
                    _threadNumbers[i] = true;
                    ReserveNumber = i;


                    return true;
                }
                else if (i == _threadNumbers.Length - 1)
                {
                    ReserveNumber = -1; //stop trying to start new jobs
                    return false;
                }
            }
            ReserveNumber = -1; //stop trying to start new jobs
            return false;
        }

        /// <summary>
        /// Unreserve a thread number.
        /// </summary>
        static void UnReserveThreadNumber(int JobNumber)
        {
            _threadNumbers[JobNumber] = false;

        }

        /// <summary>
        /// A internal class to hold a job to be called on another thread.
        /// </summary>
        private class MultithreadingJob
        {
            // The job to do on another thread.
            protected object job;

            // The Parameters needed in the job method.
            protected object[] jobParameters;

            // The number of the thread, can only have up-to thread count.
            protected int threadNumber;

            /// <summary>
            /// The Constructor
            /// </summary>
            /// <param name="FuncOtherTread">The Function to call on an other tread. NOTE: This function's first Parameter must be an int which the MultithreadingJobsHandler will add a thread number (or current job number), 
            /// to be used inside of the function to separate it from the same function running on a different thread (job).</param>
            /// <param name="FuncOtherTreadParameters">The parameters of the function being called on the other tread. NOTE: The MultithreadingJobsHandler will add an int to the first of this.</param>
            public MultithreadingJob(object FuncOtherTread, object[] FuncOtherTreadParameters)
            {
                job = FuncOtherTread;

                jobParameters = FuncOtherTreadParameters;

            }


            /// <summary>
            /// Handle the invoking the job.
            /// </summary>
            public virtual void MultithreadingJobHandler()
            {

                try
                {
                    Delegate JobDelegate = job as Delegate;

                    JobDelegate.DynamicInvoke(AddJobNumberToJobParameters());

                }
                catch (Exception ex)
                {

                    HandleError(ex);
                }


            }

            /// <summary>
            /// Add thread number to the class.
            /// </summary>
            /// <param name="Number"></param>
            public void AddJobNumber(int Number)
            {
                threadNumber = Number;
            }

            /// <summary>
            /// Add the thread number to the front of the object array.
            /// </summary>
            /// <returns></returns>
            protected object[] AddJobNumberToJobParameters()
            {

                object[] temp = new object[jobParameters.Length + 1];

                temp[0] = threadNumber;
                jobParameters.CopyTo(temp, 1);

                return temp;

            }

        }

        /// <summary>
        /// A internal class to hold a job to be called on another thread an then call a return function.
        /// </summary>
        private class MultithreadingJobWithReturn : MultithreadingJob
        {
            // The delegate to return the calculations.
            object returnDelegate;

            /// <summary>
            /// The Constructor
            /// </summary>
            /// <param name="FuncOtherTread">The Function to call on an other tread. NOTE: This function's first Parameter must be an int which the MultithreadingJobsHandler will add a thread number (or current job number), 
            /// to be used inside of the function to separate it from the same function running on a different thread (job).</param>
            /// <param name="FuncOtherTreadParameters">The parameters of the function being called on the other tread. NOTE: The MultithreadingJobsHandler will add an int to the first of this.</param>
            /// <param name="returnDelegate">The function to call when the first thread returns its calculations. (NOTE: The input parameters must be the same as the return parameters of FuncOtherTread.)</param>
            public MultithreadingJobWithReturn(object FuncOtherTread, object[] FuncOtherTreadParameters, object returnDelegate) : base(FuncOtherTread, FuncOtherTreadParameters)
            {
                this.returnDelegate = returnDelegate;
            }

            /// <summary>
            /// Invoke the job and then invoke the method the calculations are being outputted to. 
            /// </summary>
            public override void MultithreadingJobHandler()
            {


                try
                {
                    Delegate JobDelegate = job as Delegate;

                    dynamic result = JobDelegate.DynamicInvoke(AddJobNumberToJobParameters());

                    Delegate returnFunction = returnDelegate as Delegate;

                    returnFunction.DynamicInvoke(result);

                }
                catch (Exception ex)
                {

                    HandleError(ex);

                }

            }


        }

        static void HandleError(Exception ex)
        {

            if (ErrorReport == null)
            {
                Console.Write(ex.ToString());
            }
            else
            {
                ErrorReport(ex);
            }

        }

    }

    public delegate void ErrorCall(Exception Error);


}


