using System;
using System.Net;
using System.Threading;
using System.Collections.Specialized;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;
using System.IO;
using System.Collections.Generic;
namespace VPet.Plugin.API {
    public class VPET_API {
        IMainWindow MW;

        public VPET_API(IMainWindow window) {
            MW = window;
        }
        public string WorkTimerDisplayType(Dictionary<string,string> param) {
            int type = MW.Main.WorkTimer.DisplayType;
            if (param.ContainsKey("type")) {
                MW.Main.WorkTimer.DisplayType = int.Parse(param["type"]);
            }
            return Convert.ToString(type);
        }
        public string WorkTimerGetCount(Dictionary<string,string> param) {
            double count = MW.Main.WorkTimer.GetCount;
            if (param.ContainsKey("count")) {
                MW.Main.WorkTimer.GetCount = double.Parse(param["count"]);
            }
            return Convert.ToString(count);
        }
        public string WorkTimerStartTime(Dictionary<string,string> param) {
            return  MW.Main.WorkTimer.StartTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string PlayingVoice(Dictionary<string,string> param) {
            return Convert.ToString(MW.Main.PlayingVoice);
        }
        public string CleanState(Dictionary<string,string> param) {
            MW.Main.CleanState();
            return Convert.ToString(true);
        }
        public string DisplayType(Dictionary<string,string> param) {
            return Convert.ToString(MW.Main.DisplayType);
        }
        public string CountNomal(Dictionary<string,string> param) {
            return Convert.ToString(MW.Main.CountNomal);
        }
        public string DisplayToNomal(Dictionary<string,string> param) {
            MW.Main.DisplayToNomal();
            return Convert.ToString(true);
        }
        public string DisplayStop(Dictionary<string,string> param) {
            MW.Main.DisplayStop(new Action(() => {}));
            return Convert.ToString(true);
        }
        public string DisplayMove(Dictionary<string,string> param) {
            MW.Main.DisplayMove();
            return Convert.ToString(true);
        }
        public string DisplayTouchHead(Dictionary<string,string> param) {
            MW.Main.DisplayTouchHead();
            return Convert.ToString(true);
        }
        public string DisplayTouchBody(Dictionary<string,string> param) {
            MW.Main.DisplayTouchBody();
            return Convert.ToString(true);
        }
        public string DisplayIdel_StateONE(Dictionary<string,string> param) {
            MW.Main.DisplayIdel_StateONE();
            return Convert.ToString(true);
        }
        public string DisplayIdel_StateTWO(Dictionary<string,string> param) {
            MW.Main.DisplayIdel_StateTWO(param["name"]);
            return Convert.ToString(true);
        }
        public string DisplayIdel(Dictionary<string,string> param) {
            MW.Main.DisplayIdel();
            return Convert.ToString(true);
        }
        public string DisplayBLoopingToNomal(Dictionary<string,string> param) {
            if (param.ContainsKey("graphname"))
                MW.Main.DisplayBLoopingToNomal(param["graphname"],int.Parse(param["loopLength"]));
            else
                MW.Main.DisplayBLoopingToNomal(int.Parse(param["loopLength"]));
            return Convert.ToString(true);
        }
        public string DisplaySleep(Dictionary<string,string> param) {
            MW.Main.DisplaySleep(bool.Parse(param["force"]));
            return Convert.ToString(true);
        }
        public string DisplayBLoopingForce(Dictionary<string,string> param) {
            MW.Main.DisplayBLoopingForce(param["graphname"]);
            return Convert.ToString(true);
        }
        public string DisplayRaised(Dictionary<string,string> param) {
            MW.Main.DisplayRaised();
            return Convert.ToString(true);
        }
        public string DisplayCEndtoNomal(Dictionary<string,string> param) {
            MW.Main.DisplayCEndtoNomal(param["graphname"]);
            return Convert.ToString(true);
        }
        public string Display(Dictionary<string,string> param) {
            //TODO
            return "todo";
        }
        public string OnSay(Dictionary<string,string> param) {
            //TODO
            return "todo";
        }
        public string LastInteractionTime(Dictionary<string,string> param) {
            return MW.Main.LastInteractionTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public string Say(Dictionary<string,string> param) {
            MW.Main.Say(param["text"],param["graphname"],bool.Parse(param["force"]));
            return Convert.ToString(true);
        }
        public string LabelDisplayShow(Dictionary<string,string> param) {
            MW.Main.LabelDisplayShow(param["text"],int.Parse(param["time"]));
            return Convert.ToString(true);
        }
        public string LabelDisplayShowChangeNumber(Dictionary<string,string> param) {
            MW.Main.LabelDisplayShowChangeNumber(param["text"],double.Parse(param["changenum1"]),double.Parse(param["changenum2"]),int.Parse(param["time"]));
            return Convert.ToString(true);
        }
        public string FunctionSpend(Dictionary<string,string> param) {
            MW.Main.FunctionSpend(double.Parse(param["TimePass"]));
            return Convert.ToString(true);
        }
        public string RandomInteractionAction(Dictionary<string,string> param) {
            string return_str = "[";
            foreach (Func<bool> value in MW.Main.RandomInteractionAction)
                return_str += Convert.ToString(value()) + ",";
            return_str += "]";
            return return_str;
        }
        public string FunctionSpendHandle(Dictionary<string,string> param) {
            //TODO
            return "todo";
        }
        public string EventTimer_Elapsed(Dictionary<string,string> param) {
            MW.Main.EventTimer_Elapsed();
            return Convert.ToString(true);
        }
        public string SetMoveTimerPoint(Dictionary<string,string> param) {
            if (param.ContainsKey("x"))
                MW.Main.MoveTimerPoint.X = double.Parse(param["x"]);
            if (param.ContainsKey("y"))
                MW.Main.MoveTimerPoint.Y = double.Parse(param["y"]);
            return Convert.ToString(true);
        }
        public string GetMoveTimerPoint(Dictionary<string,string> param) {
            double x,y;
            x = MW.Main.MoveTimerPoint.X;
            y = MW.Main.MoveTimerPoint.Y;
            return string.Format("[{0},{1}]",x,y);
        }
        public string SetLogicInterval(Dictionary<string,string> param) {
            MW.Main.SetLogicInterval(int.Parse(param["Interval"]));
            return Convert.ToString(true);
        }
        public string SetMoveMode(Dictionary<string,string> param) {
            MW.Main.SetMoveMode(bool.Parse(param["AllowMove"]),bool.Parse(param["smartMove"]),int.Parse(param["SmartMoveInterval"]));
            return Convert.ToString(true);
        }
        public string State(Dictionary<string,string> param) {
            //TODO
            return "todo";
        }
        public string StateID(Dictionary<string,string> param) {
            return Convert.ToString(MW.Main.StateID);
        }
        public string nowWork(Dictionary<string,string> param) {
            //TODO
            return Convert.ToString(MW.Main.nowWork);
        }
        public string MessageBarShow(Dictionary<string,string> param) {
            MW.Main.MsgBar.Show(param["name"],param["text"]);
            return Convert.ToString(true);
        }
        public string MessageBarForceClose(Dictionary<string,string> param) {
            MW.Main.MsgBar.ForceClose();
            return Convert.ToString(true);
        }
        public string AddMenuButton(Dictionary<string,string> param) {
            //TODO
            return "todo";
        }
        public string Resolution(Dictionary<string,string> param) {
            int res = MW.Core.Graph.Resolution;
            //TODO
            return Convert.ToString(res);
        }
        public string GraphsName(Dictionary<string,string> param) {
            var name = MW.Core.Graph.GraphsName;
            //TODO
            return Convert.ToString(name);
        }
        public string GraphsList(Dictionary<string,string> param) {
            var list = MW.Core.Graph.GraphsList;
            //TODO
            return Convert.ToString(list);
        }
        public string CommUIElements(Dictionary<string,string> param) {
            var elem = MW.Core.Graph.CommUIElements;
            //TODO
            return Convert.ToString(elem);
        }
        public Dictionary<string,Func<Dictionary<string, string>, string>> GetAPI() {
            Dictionary<string,Func<Dictionary<string, string>, string>> funcmap = new Dictionary<string,Func<Dictionary<string, string>, string>>();
            funcmap["WorkTimerDisplayType"] = WorkTimerDisplayType;
            funcmap["WorkTimerGetCount"] = WorkTimerGetCount;
            funcmap["WorkTimerStartTime"] = WorkTimerStartTime;
            funcmap["PlayingVoice"] = PlayingVoice;
            funcmap["CleanState"] = CleanState;
            funcmap["DisplayType"] = DisplayType;
            funcmap["CountNomal"] = CountNomal;
            funcmap["DisplayToNomal"] = DisplayToNomal;
            funcmap["DisplayStop"] = DisplayStop;
            funcmap["DisplayMove"] = DisplayMove;
            funcmap["DisplayTouchHead"] = DisplayTouchHead;
            funcmap["DisplayTouchBody"] = DisplayTouchBody;
            funcmap["DisplayIdel_StateONE"] = DisplayIdel_StateONE;
            funcmap["DisplayIdel_StateTWO"] = DisplayIdel_StateTWO;
            funcmap["DisplayIdel"] = DisplayIdel;
            funcmap["DisplayBLoopingToNomal"] = DisplayBLoopingToNomal;
            funcmap["DisplaySleep"] = DisplaySleep;
            funcmap["DisplayBLoopingForce"] = DisplayBLoopingForce;
            funcmap["DisplayRaised"] = DisplayRaised;
            funcmap["DisplayCEndtoNomal"] = DisplayCEndtoNomal;
            funcmap["Display"] = Display;
            funcmap["LastInteractionTime"] = LastInteractionTime;
            funcmap["Say"] = Say;
            funcmap["LabelDisplayShow"] = LabelDisplayShow;
            funcmap["LabelDisplayShowChangeNumber"] = LabelDisplayShowChangeNumber;
            funcmap["FunctionSpend"] = FunctionSpend;
            funcmap["RandomInteractionAction"] = RandomInteractionAction;
            funcmap["EventTimer_Elapsed"] = EventTimer_Elapsed;
            funcmap["SetMoveTimerPoint"] = SetMoveTimerPoint;
            funcmap["GetMoveTimerPoint"] = GetMoveTimerPoint;
            funcmap["SetLogicInterval"] = SetLogicInterval;
            funcmap["SetMoveMode"] = SetMoveMode;
            funcmap["StateID"] = StateID;
            funcmap["nowWork"] = nowWork;
            funcmap["MessageBarShow"] = MessageBarShow;
            funcmap["Resolution"] = Resolution;
            return funcmap;
        }
    }
}