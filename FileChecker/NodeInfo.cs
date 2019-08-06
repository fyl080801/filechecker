using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChecker
{
    public class NodeInfo
    {
        private List<NodeInfo> _children;

        public string Name { get; set; }

        public string Fullpath { get; set; }

        public bool IsFile { get; set; }

        public string Ext { get; set; }

        public List<NodeInfo> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<NodeInfo>();
                }
                return _children;
            }
            set { _children = value; }
        }
    }
}
