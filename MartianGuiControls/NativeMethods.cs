// Published under http://www.opensource.org/licenses/BSD-3-Clause license, see license.txt file for details.

using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MartianGuiControls.Native
{
	// http://msdn.microsoft.com/en-us/library/bb775514.aspx
	[StructLayout(LayoutKind.Sequential)]
	internal struct NMHDR
	{
		public IntPtr hwndFrom;
		public IntPtr idFrom;
		public int code;
	}

	// http://msdn.microsoft.com/en-us/library/bb773456.aspx
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct TVITEM
	{
		public int mask;
		public IntPtr hItem;
		public int state;
		public int stateMask;
		public IntPtr pszText;
		public int cchTextMax;
		public int iImage;
		public int iSelectedImage;
		public int cChildren;
		public IntPtr lParam;
	}

	// http://msdn.microsoft.com/en-us/library/bb773452.aspx
	[StructLayout(LayoutKind.Sequential)]
	internal struct TVINSERTSTRUCT
	{
		public IntPtr hParent;
		public IntPtr hInsertAfter;
		public TVITEM item;
	}

	// http://msdn.microsoft.com/en-us/library/bb773411.aspx
	[StructLayout(LayoutKind.Sequential)]
	internal struct NMTREEVIEW
	{
		public NMHDR nmhdr;
		public int action;
		public TVITEM itemOld;
		public TVITEM itemNew;
		public Point ptDrag;
	}

	// http://msdn.microsoft.com/en-us/library/bb773418.aspx
	[StructLayout(LayoutKind.Sequential)]
	internal struct NMTVDISPINFO
	{
		public NMHDR hdr;
		public TVITEM item;
	}

	internal enum WM: int
	{
		WM_NOTIFY = 0x004E,
		WM_REFLECTED = 0x2000
	}

	internal static class TVI
	{
		internal static readonly IntPtr TVI_FIRST = new IntPtr(-0x0FFFF);
		internal static readonly IntPtr TVI_LAST = new IntPtr(-0x0FFFE);
		internal static readonly IntPtr TVI_ROOT = new IntPtr(-0x10000);
		internal static readonly IntPtr TVI_SORT = new IntPtr(-0x0FFFD);
	}

	internal enum TVN: int
	{
		// http://msdn.microsoft.com/en-us/library/system.windows.forms.control.wndproc.aspx
		// http://msdn.microsoft.com/en-us/library/bb773512.aspx
		TVN_FIRST = -400,
		TVN_GETDISPINFO = TVN_FIRST - 52
	}

	internal static class CallbackTypes
	{
		internal static readonly IntPtr LPSTR_TEXTCALLBACK = new IntPtr(-1);
		internal const int I_IMAGECALLBACK = -1;
		internal const int I_CHILDRENCALLBACK = -1;
		internal const int I_CHILDRENAUTO = -2;
	}

	[Flags]
	internal enum TVIF: int
	{
		TVIF_TEXT = 0x0001,
		TVIF_IMAGE = 0x0002,
		TVIF_PARAM = 0x0004,
		TVIF_STATE = 0x0008,
		TVIF_HANDLE = 0x0010,
		TVIF_SELECTEDIMAGE = 0x0020,
		TVIF_CHILDREN = 0x0040
	}


	internal enum TVM: int
	{
		TV_FIRST = 0x1100,
		TVM_DELETEITEM = TV_FIRST + 1,
		TVM_INSERTITEM = TV_FIRST + 50,
		TVM_GETITEM = TV_FIRST + 62,
		TVM_SETITEM = TV_FIRST + 63
	}


	internal static class NativeFunc
	{
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);

		internal static bool SetTVItem(IntPtr hWnd, TVITEM item)
		{
			IntPtr res = SendMessage(hWnd, (int)TVM.TVM_SETITEM, IntPtr.Zero, ref item);
			return !IntPtr.Zero.Equals(res);
		}

		internal static bool GetTVItem(IntPtr hWnd, ref TVITEM item)
		{
			IntPtr res = SendMessage(hWnd, (int)TVM.TVM_GETITEM, IntPtr.Zero, ref item);
			return !IntPtr.Zero.Equals(res);
		}

		internal static bool SetTVItemChildren(IntPtr hWnd, IntPtr hItem, int children)
		{
			TVITEM item = new TVITEM();
			item.mask = (int)TVIF.TVIF_CHILDREN;
			item.hItem = hItem;
			item.cChildren = (int)CallbackTypes.I_CHILDRENCALLBACK;
			return NativeFunc.SetTVItem(hWnd, item);
		}

		internal static bool GetTVItemChildren(IntPtr hWnd, IntPtr hItem, out int children)
		{
			TVITEM item = new TVITEM();
			item.mask = (int)TVIF.TVIF_CHILDREN;
			item.hItem = hItem;
			bool res = NativeFunc.GetTVItem(hWnd, ref item);
			children = res ? item.cChildren : 0;
			return res;
		}
	}
}
