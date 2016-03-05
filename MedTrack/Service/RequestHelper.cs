using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MedTrack;

namespace MedTrack.Service
{
    class RequestHelper
    {
        public LandingRequestObject PopulateLandingRequestObject()
        {
            LandingRequestObject obj = new LandingRequestObject();
            obj.apiKey = "QmjNMCGWwezHKUAedDGXkDaGqOv3HBej";
            obj.affId = "extest1";
            obj.transaction = "refillByScan";
            obj.act = "mweb5Url";
            obj.view = "mweb5UrlJSON";
            obj.appver = "Android,5.0";
            obj.devinf = "3.1";
            return obj;
        }

        public RefillRequestObject PopulateRefillRequestObject()
        {
            RefillRequestObject obj = new RefillRequestObject();
            obj.affId = "extest1";
            obj.appId = "refillByScan";
            obj.act = "chkExpRx";
            obj.lat = "42.165526";
            obj.lng = "-87.847672";
            obj.appCallBackScheme = "refillByScan://handleControl";
            obj.appCallBackAction = "callBackAction";
            obj.devinf = "Android,5.0";
            obj.appver = "3.1";
            //obj.rxNo = "0483557-59381";
            return obj;
        }
    }
}