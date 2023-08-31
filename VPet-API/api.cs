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
            return Convert.ToString(MW.Main.WorkTimer.DisplayType);
        }
        public string WorkTimerGetCount(Dictionary<string,string> param) {
            return Convert.ToString(MW.Main.WorkTimer.GetCount);
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
            return funcmap;
        }
    }
}