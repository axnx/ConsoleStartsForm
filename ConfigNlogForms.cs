using NLog;
using NLog.Config;
using NLog.Targets.Wrappers;
using NLog.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStartsForm
{


    //We have to install https://www.nuget.org/packages/NLog.Windows.Forms
    //Install-Package NLog.Windows.Forms
    //After this use NLog.Windows.Forms;
    class ConfigNlogForms
    {
        public static void config()
        {
            NLog.Windows.Forms.RichTextBoxTarget target = new NLog.Windows.Forms.RichTextBoxTarget();
            target.Name = "rtbConsole";
            target.Layout = "${longdate} ${level:uppercase=true} ${logger} ${message}";
            target.ControlName = "richTextBoxMainLog";
            target.FormName = "FormMain";
            target.AutoScroll = true;
            target.MaxLines = 10000;
            target.UseDefaultRowColoringRules = false;
            target.RowColoringRules.Add(
                new RichTextBoxRowColoringRule(
                    "level == LogLevel.Trace", // condition
                    "DarkGray", // font color
                    "Control", // background color
                    FontStyle.Regular
                )
            );
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Debug", "Gray", "Control"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Info", "ControlText", "Control"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Warn", "DarkRed", "Control"));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Error", "White", "DarkRed", FontStyle.Bold));
            target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Fatal", "Yellow", "DarkRed", FontStyle.Bold));

            AsyncTargetWrapper asyncWrapper = new AsyncTargetWrapper();
            asyncWrapper.Name = "AsyncRichTextBox";
            asyncWrapper.WrappedTarget = target;

            SimpleConfigurator.ConfigureForTargetLogging(asyncWrapper, LogLevel.Trace);


        }

    }
}
