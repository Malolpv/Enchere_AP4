using System;
using System.Collections.Generic;
using System.Text;

namespace Enchere_AP4
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);

    }
}
