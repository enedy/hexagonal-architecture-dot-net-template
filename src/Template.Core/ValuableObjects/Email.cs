using System;
using System.Text.RegularExpressions;

namespace Template.Shared.Core.ValuableObjects
{
    public record Email
    {
        public Email(string addrees)
        {
            Addrees = addrees;

            if(!IsValidEmail(addrees))
                throw new ArgumentException("address");
        }

        public string Addrees { get; private set; }

        public bool IsValidEmail(string address)
        {
            var patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
               + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
               + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
               + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
               + @"[a-zA-Z]{2,}))$";

            return new Regex(patternStrict).IsMatch(address);
        }
    }
}
