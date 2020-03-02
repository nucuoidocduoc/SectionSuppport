using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SectionSupport
{
    public class FilterFactory
    {
        private ConditionHidden _condition;

        public FilterFactory(ConditionHidden condition)
        {
            _condition = condition;
        }

        public IFilter CreateFilter()
        {
            switch (_condition.FilterType) {
                case FilterType.Contain:
                    return new ContainFilter(_condition);

                case FilterType.StartWith:
                    return new StartWithFilter(_condition);

                case FilterType.EndWith:
                    return new EndWithFilter(_condition);

                default:
                    return null;
            }
        }
    }
}