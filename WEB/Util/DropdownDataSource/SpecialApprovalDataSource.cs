using System.Collections.Generic;
using WEB.Models;

namespace WEB.Util.DropdownDataSource
{
    public class SpecialApprovalDataSource
    {
        public static List<ValueTextModel> DataSource
        {
            get
            {
                var list = new List<ValueTextModel>
                {
                    new ValueTextModel{Value = "0.0010",Text = "0.1%"},
                    new ValueTextModel{Value = "0.0020",Text = "0.2%"},
                    new ValueTextModel{Value = "0.0030",Text = "0.3%"},
                    new ValueTextModel{Value = "0.0040",Text = "0.4%"},
                    new ValueTextModel{Value = "0.0050",Text = "0.5%"},
                    new ValueTextModel{Value = "0.0060",Text = "0.6%"},
                    new ValueTextModel{Value = "0.0070",Text = "0.7%"},
                    new ValueTextModel{Value = "0.0080",Text = "0.8%"},
                    new ValueTextModel{Value = "0.0090",Text = "0.9%"},
                    new ValueTextModel{Value = "0.0100",Text = "1.0%"}
                };
                return list;
            }
        }
    }
}
