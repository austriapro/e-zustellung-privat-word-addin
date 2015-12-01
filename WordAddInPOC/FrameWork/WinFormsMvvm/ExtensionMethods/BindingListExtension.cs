using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WinFormsMvvm.ExtensionMethods
{
    public static class BindingListExtension
    {
        /// <summary>
        /// Adds all the data to a binding list
        /// </summary>
        public static void AddRange<T>(this BindingList<T> list, IEnumerable<T> data)
        {
            if (list == null || data == null)
            {
                return;
            }

            foreach (T t in data)
            {
                list.Add(t);
            }
        }
    }
}
