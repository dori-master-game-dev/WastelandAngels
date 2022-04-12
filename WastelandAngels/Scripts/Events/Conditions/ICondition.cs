using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLA.GameComponents;

namespace WLA.Events.Conditions
{
    public interface ICondition
    {
        bool CheckCondition(Level level);
    }
}
