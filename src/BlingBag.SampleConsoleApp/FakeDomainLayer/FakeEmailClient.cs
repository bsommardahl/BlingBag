using System;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer
{
    public class FakeEmailClient : IEmailClient
    {
        public void Send(string emailAddress, string subject, string body)
        {
            Console.WriteLine(string.Format("## (FakeEmailClient) -- Email was sent to {0}. Subject : {1} - Body: {2}",
                                            emailAddress, subject, body));
        }
    }
}