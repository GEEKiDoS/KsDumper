using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KsDumperClient.Utility
{
    public class ModuleListView : ListView
    {
        private int sortColumnIndex = 1;
        private ModuleSummary[] moduleCache;

        public ModuleListView()
        {
            DoubleBuffered = true;
            Sorting = SortOrder.Ascending;
        }

        public void LoadModules(ModuleSummary[] moduleSummaries)
        {
            moduleCache = moduleSummaries;
            ReloadItems();
        }

        public void ShowSystemModulees()
        {
            ReloadItems();
        }

        public void HideSystemModulees()
        {
            ReloadItems();
        }

        private void ReloadItems()
        {
            Items.Clear();

            string systemRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows).ToLower();

            foreach (ModuleSummary moduleSummary in moduleCache)
            {
                ListViewItem lvi = new ListViewItem(string.Format("0x{0:x8}", moduleSummary.ModuleBase));
                lvi.SubItems.Add(moduleSummary.ModuleFileName);
                lvi.SubItems.Add(string.Format("0x{0:x8}", moduleSummary.ModuleEntryPoint));
                lvi.SubItems.Add(string.Format("0x{0:x4}", moduleSummary.ModuleImageSize));
                lvi.Tag = moduleSummary;

                Items.Add(lvi);
            }

            ListViewItemSorter = new ModuleListViewItemComparer(sortColumnIndex, Sorting);
            Sort();
        }

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            if (e.Column != sortColumnIndex)
            {
                sortColumnIndex = e.Column;
                Sorting = SortOrder.Ascending;
            }
            else
            {
                if (Sorting == SortOrder.Ascending)
                {
                    Sorting = SortOrder.Descending;
                }
                else
                {
                    Sorting = SortOrder.Ascending;
                }
            }

            ListViewItemSorter = new ModuleListViewItemComparer(e.Column, Sorting);
            Sort();
        }

        private class ModuleListViewItemComparer : IComparer
        {
            private readonly int columnIndex;
            private readonly SortOrder sortOrder;

            public ModuleListViewItemComparer(int columnIndex, SortOrder sortOrder)
            {
                this.columnIndex = columnIndex;
                this.sortOrder = sortOrder;
            }

            public int Compare(object x, object y)
            {
                if ((x is ListViewItem) && (y is ListViewItem))
                {
                    ModuleSummary p1 = ((ListViewItem)x).Tag as ModuleSummary;
                    ModuleSummary p2 = ((ListViewItem)y).Tag as ModuleSummary;

                    if (!(p1 == null || p2 == null))
                    {
                        int result = 0;

                        switch (columnIndex)
                        {
                            case 0:
                                result = p1.ModuleBase.CompareTo(p2.ModuleBase);
                                break;
                            case 1:
                                result = p1.ModuleFileName.CompareTo(p2.ModuleFileName);
                                break;
                            case 2:
                                result = p1.ModuleEntryPoint.CompareTo(p2.ModuleEntryPoint);
                                break;
                            case 3:
                                result = p1.ModuleImageSize.CompareTo(p2.ModuleImageSize);
                                break;
                        }

                        if (sortOrder == SortOrder.Descending)
                        {
                            result = -result;
                        }
                        return result;
                    }
                }
                return 0;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1)
            {
                SetWindowTheme(Handle, "Explorer", null);
            }
            base.WndProc(ref m);
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);       
    }
}
