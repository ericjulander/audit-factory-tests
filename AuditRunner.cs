using System;
using System.Collections.Generic;
using ConsoleReport;
namespace Auditor
{
    public delegate StatusCollection AuditResultProcess(StatusCollection statusObjects);

    public static class AuditRunner
    {

        public delegate AuditResultProcess AuditMethod<T>(T item);

        public static StatusCollection RunAudits<T>(string title, T AuditObject, List<AuditMethod<T>> audits)
        {
            var results = new StatusCollection(title);
            while (audits.Count > 0)
            {
                results = audits[0](AuditObject)(results);
                audits.RemoveAt(0);
            }
            return results;
        }
    }

    public class StatusObject
    {

        /* 
            Status Code:
            0 - Success
            1 - Warning
            2 - Failure
        */
        private int _statusCode;
        public int StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                if (value >= 0 && value <= 3)
                {
                    _statusCode = value;
                }
                else
                {
                    _statusCode = -1;
                }
            }
        }

        /*
         * Status Message
         * Message accompanying the status code. 
         * Description of the result.
         */
        private string _statusMessage;
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
            }
        }
        /*
         * Status Title
         * Title of the process run.
         */
        private string _statusTitle;
        public string StatusTitle
        {
            get
            {
                return _statusTitle;
            }
            set
            {
                _statusTitle = value;
            }
        }

        public StatusObject(int status, string title, string message)
        {
            StatusCode = status;
            StatusTitle = title;
            StatusMessage = message;
        }

        public void PrintStatusMessage()
        {
            var color = (this.StatusCode == 0) ? ConsoleColor.Green : (this.StatusCode == 1) ? ConsoleColor.Yellow : ConsoleColor.Red;
            var status = (this.StatusCode == 0) ? "SUCCESS" : (this.StatusCode == 1) ? "WARNING" : "FAIL";

            var ReportItemz = new List<ReportItem>(){
                new ReportItem($" The Audit \"{StatusTitle}\" resulted with the status: \"{status}\"!",color, (color.Equals(ConsoleColor.Yellow) ? ConsoleColor.DarkBlue : ConsoleColor.White) ),
                new ReportItem(StatusMessage, ConsoleColor.Black)
            };

            ConsoleRep.Log(ReportItemz);
        }
    }

    public class StatusCollection
    {
        /*
         * Status Title
         * Title of the process run.
         */
        private string _collectionTitle;
        public string CollectionTitle
        {
            get
            {
                return _collectionTitle;
            }
            set
            {
                _collectionTitle = value;
            }
        }

        public List<StatusObject> StatusObjects { get; set; }

        public StatusCollection(string title)
        {
            _collectionTitle = title;
            StatusObjects = new List<StatusObject>();
        }
        public StatusCollection(string title, List<StatusObject> statusObjects)
        {
            _collectionTitle = title;
            StatusObjects = statusObjects;
        }
    }

}