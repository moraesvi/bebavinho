using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Helper
{
    public class EmailHelper
    {
        private static MailMessage _objMailMessage;

        private static SmtpClient _objSmtpClient;

        private static Email _objEmail = new Email();

        private static Configuration _objConfiguration = new Configuration();

        public static Email Email
        {
            get
            {
                return _objEmail;
            }
            set
            {
                _objEmail = value;
            }
        }

        public static Configuration Configuration
        {
            get
            {
                return _objConfiguration;
            }
            set
            {
                _objConfiguration = value;
            }
        }

        public static bool SendEmail()
        {
            _objMailMessage = new MailMessage();
            _objMailMessage.To.Add(_objEmail.To);
            _objMailMessage.From = _objEmail.From;
            _objMailMessage.Subject = _objEmail.Subject;
            _objMailMessage.SubjectEncoding = _objEmail.SubjectEncoding;
            _objMailMessage.Body = _objEmail.Body;
            _objMailMessage.BodyEncoding = _objEmail.BodyEncoding;
            _objMailMessage.IsBodyHtml = _objEmail.IsBodyHtml;
            _objMailMessage.Priority = _objEmail.Priority;

            _objSmtpClient = new SmtpClient();
            _objSmtpClient.Credentials = _objConfiguration.Credentials;
            _objSmtpClient.Port = _objConfiguration.Port;
            _objSmtpClient.Host = _objConfiguration.Host;
            _objSmtpClient.EnableSsl = _objConfiguration.EnableSsl;

            try
            {
                _objSmtpClient.Send(_objMailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Exception exFormated = ex;
                string errorMessage = string.Empty;
                while (exFormated != null)
                {
                    errorMessage += exFormated.ToString();
                    exFormated = exFormated.InnerException;
                }
                throw new Exception(errorMessage);
            }
        }
    }

    public class Email
    {
        public string _to;

        public List<string> _toCollection;

        public MailAddress _from;

        public string _subject;

        public MailPriority _priority;

        public string _body;

        public bool _isBodyHtml;

        private System.Text.Encoding _subjectEncoding;

        private System.Text.Encoding _bodyEncoding;

        public Email()
        {
            _isBodyHtml = true;
            _from = new MailAddress(string.Empty);
            _subjectEncoding = System.Text.Encoding.UTF8;
            _bodyEncoding = System.Text.Encoding.UTF8;
            _priority = MailPriority.Normal;
        }

        public string To
        {
            get
            {
                StringIsNull(_to, "É obrigatório definir o campo To");
                return _to;
            }
            set
            {
                _to = value;
            }
        }

        public List<string> ToCollection
        {
            get
            {
                CollectionIsNull<List<string>>(_toCollection, "É obrigatório definir o campo ToCollection");
                return _toCollection;
            }
            set
            {
                _toCollection = value;
            }
        }

        public MailAddress From
        {
            get
            {
                ObjectIsNull(_from, "É obrigatório definir o campo From");
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        public string Subject
        {
            get
            {
                StringIsNull(_subject, "É obrigatório definir o campo Subject");
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        /// <summary>
        /// Default normal.
        /// </summary>
        public MailPriority Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
            }
        }

        public string Body
        {
            get
            {
                StringIsNull(_subject, "É obrigatório definir o campo Body");
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        /// <summary>
        /// Default true
        /// </summary>
        public bool IsBodyHtml
        {
            get
            {
                return _isBodyHtml;
            }
            set
            {
                _isBodyHtml = value;
            }
        }

        /// <summary>
        /// Default UTF8;
        /// </summary>
        public System.Text.Encoding SubjectEncoding
        {
            get
            {
                return _subjectEncoding;
            }
            set
            {
                _subjectEncoding = value;
            }
        }

        /// <summary>
        /// Default UTF8;
        /// </summary>
        public System.Text.Encoding BodyEncoding
        {
            get
            {
                return _bodyEncoding;
            }
            set
            {
                _bodyEncoding = value;
            }
        }

        private void ObjectIsNull(object value, string message)
        {
            if (value == null)
            {
                throw new InvalidOperationException(message);
            }
        }

        private void StringIsNull(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(message);
            }
        }

        private void CollectionIsNull<T>(System.Collections.IEnumerable value, string message) where T : System.Collections.IEnumerable
        {
            if (value == null)
            {
                throw new InvalidOperationException(message);
            }
            if (value.Cast<T>().Count() == 0)
            {
                throw new InvalidOperationException(message);
            }
        }
    }

    public class Configuration
    {
        private int _port;

        private string _host;

        private bool _enableSsl;

        private System.Net.NetworkCredential _credentials;

        public Configuration()
        {
            _enableSsl = false;
        }

        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public string Host
        {
            get
            {
                StringIsNull(_host, "É obrigatório definir o campo Host");
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        /// <summary>
        /// Default false;
        /// </summary>
        public bool EnableSsl
        {
            get
            {
                return _enableSsl;
            }
            set
            {
                _enableSsl = value;
            }
        }

        public System.Net.NetworkCredential Credentials
        {
            get
            {
                ObjectIsNull(_credentials, "É obrigatório definir o campo Credentials");
                return _credentials;
            }
            set
            {
                _credentials = value;
            }
        }

        private void ObjectIsNull(object value, string message)
        {
            if (value == null)
            {
                throw new InvalidOperationException(message);
            }
        }

        private void StringIsNull(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(message);
            }
        }

    }
}
