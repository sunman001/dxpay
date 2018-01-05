using System;
using System.Drawing;
using System.Windows.Forms;

namespace JmPay.PayChannelMonitor.Util.RichTextBoxExtensions
{
    /// <summary>
    ///  RichTextBox静态扩展类
    /// </summary>
    public static class ColorRichTextBox
    {
        /// <summary>
        /// RichTextBox静态扩展方法,可改变富文本框的前景色
        /// </summary>
        /// <param name="box">富文本框</param>
        /// <param name="text">文本内容</param>
        /// <param name="color">前景色</param>
        /// <param name="addNewLine">是否自动换行(默认:true)</param>
        public static void AppendColorfulText(this RichTextBox box, string text, Color color, bool addNewLine = true)
        {
            if (addNewLine)
            {
                text += Environment.NewLine;
            }

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
