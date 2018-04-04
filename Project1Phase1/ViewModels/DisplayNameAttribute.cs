using System;

namespace Project1Phase1.ViewModels
{
    internal class DisplayNameAttribute : Attribute
    {
        private string v;

        public DisplayNameAttribute(string v)
        {
            this.v = v;
        }
    }
}