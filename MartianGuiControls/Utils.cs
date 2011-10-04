// Published under http://www.opensource.org/licenses/BSD-3-Clause license, see license.txt file for details.

using System.Collections.Generic;

namespace MartianGuiControls
{
	internal class Set<T>
	{
		private Dictionary<T, object> _d = new Dictionary<T, object>(); // TODO change container

		public void Add(T v)
		{
			_d.Add(v, null);
		}

		public bool Contains(T v)
		{
			return _d.ContainsKey(v);
		}

		public void Remove(T v)
		{
			_d.Remove(v);
		}
	}
}
