﻿using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Cinema.Areas.Admin.Controllers
{
    public class FacilityController : Controller
    {
        public ActionResult Facility()
        {
            //var ID = (Session["Role"]).ToString();
            //if (ID == "1")
            //{
                ViewBag.name = Session["AdminID"];

                List<JObject> locations = new List<JObject>(9999);
                List<JObject> cinemas = new List<JObject>(9999);
                List<JObject> rooms = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetLocation"); // 
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        locations.Add(o);
                    }
                    ViewBag.locations = locations;
                }
                //////////////////////
                var responseMessage1 = client.GetAsync("GetCinemaInfo"); // 
                responseMessage1.Wait();
                var result1 = responseMessage1.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var readTask = result1.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        cinemas.Add(o);
                    }
                    ViewBag.cinemas = cinemas;
                }

                responseMessage = client.GetAsync("GetRoomInfo");
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        rooms.Add(o);
                    }
                    ViewBag.rooms = rooms;
                }
                return View();
            //}
            //else
            //    return RedirectToAction("Homepage", "Admin");
        }
        public ActionResult AddCinemaView()
        {
            List<JObject> locations = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }

            HttpClient clientx = new HttpClient();
            clientx.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            clientx.DefaultRequestHeaders.Accept.Clear();
            clientx.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessagex = clientx.GetAsync("GetMaxCinemaID");
            responseMessagex.Wait();
            var resultx = responseMessagex.Result;
            if (resultx.IsSuccessStatusCode)
            {
                var readTask1 = resultx.Content.ReadAsStringAsync();
                readTask1.Wait();
                //var ss= JsonConvert.DeserializeObject<T>(readTask1);
                var test = readTask1.Result;

                string x = test.ToString();
                string ID = "CNM0" + (Int32.Parse((x.Substring(x.Length-4,2)).ToString()) + 1).ToString();
                ViewBag.CinemaID = ID;
            }
            return View();
        }
        public ActionResult AddCinema()
        {

            JObject cinema = new JObject();
            string cinemaName = Request.Form["cinema-name"];
            string cinemaId = Request.Form["cinema-id"];
            string cinemaAddr = Request.Form["cinema-address"];
            string locationId = Request.Form["location-id"];
            string cinemaNumber = Request.Form["cinema-number"];
            cinema["CinemaID"] = cinemaId;
            cinema["CinemaName"] = cinemaName;
            cinema["CinemaAddress"] = cinemaAddr;
            cinema["LocationID"] = locationId;
            cinema["PhoneNumber"] = cinemaNumber;
            
            
            var content = new StringContent(JsonConvert.SerializeObject(cinema), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.PostAsync("AddCinema", content); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            return RedirectToAction("/Facility");
        }
        public ActionResult EditCinemaView(string cinemaId)
        {
            List<JObject> locations = new List<JObject>(9999);
            


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations;
            }

            responseMessage = client.GetAsync("FindCinema?id=" + cinemaId); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                
                ViewBag.cinema = JObject.Parse(readTask.Result);
            }
            ViewBag.cinemaId = cinemaId;
            return View();
        }

        public ActionResult EditCinema(string cinemaId)
        {
            List<JObject> cinemas = new List<JObject>(9999);

            JObject cinema = new JObject();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage = client.GetAsync("FindCinema?id=" + cinemaId);
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                cinema = JObject.Parse(readTask.Result);
            }
            string cinemaName = Request.Form["cinema-name"];
            string cinemaAddr = Request.Form["cinema-address"];
            string locationId = Request.Form["location-id"];
            string cinemaNumber = Request.Form["cinema-number"];
            cinema["CinemaID"] = cinemaId;
            cinema["CinemaName"] = cinemaName;
            cinema["CinemaAddress"] = cinemaAddr;
            cinema["LocationID"] = locationId;
            cinema["PhoneNumber"] = cinemaNumber;
            var content = new StringContent(JsonConvert.SerializeObject(cinema), System.Text.Encoding.UTF8, "application/json");
            var responseMessage1 = client.PutAsync("EditCinema/" + cinemaId, content); /////////////////// truyen bien 
            responseMessage.Wait();
            var result2 = responseMessage1.Result;
            return RedirectToAction("/Facility");
        }
        public ActionResult DeleteCinema(string cinemaId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.DeleteAsync("DeleteCinema?id=" + cinemaId); /////////////////// truyen bien 
            responseMessage.Wait();
            return RedirectToAction("/Facility");
            
        }
        public ActionResult AddLocationView()
        {
            List<JObject> locations = new List<JObject>(9999);
            List<JObject> MaxID = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetLocation"); // 
            responseMessage.Wait();
            var result1 = responseMessage.Result;
            if (result1.IsSuccessStatusCode)
            {
                var readTask = result1.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    locations.Add(o);
                }
                ViewBag.locations = locations; 
            }
            HttpClient clientx= new HttpClient();
            clientx.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            clientx.DefaultRequestHeaders.Accept.Clear();
            clientx.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessagex = clientx.GetAsync("GetMaxLocationID");
            responseMessagex.Wait();
            var resultx = responseMessagex.Result;
            if (resultx.IsSuccessStatusCode)
            {
                var readTask1 = resultx.Content.ReadAsStringAsync();
                readTask1.Wait();
                //var ss= JsonConvert.DeserializeObject<T>(readTask1);
                var test = readTask1.Result;
             
                string x = test.ToString();
                string LocationID = "LC0" + (Int32.Parse((x[x.Length - 3]).ToString()) + 1).ToString();
                ViewBag.LocationID = LocationID;
            }
            
            
            return View();
        }
        
        [HttpPost]
        public ActionResult AddLocation()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

          
            string ID = Request.Form["location-id"];
            string name = Request.Form["location-name"];
            JObject location = new JObject();
            
            location["LocationID"] = ID;
            location["LocationName"] = name;
            var content = new StringContent(JsonConvert.SerializeObject(location), System.Text.Encoding.UTF8, "application/json");
            var responseMessage1 = client.PostAsync("AddLocation", content);
            responseMessage1.Wait();
            return RedirectToAction("/Facility");

        }
        [HttpGet]
        public ActionResult EditLocationView(string locationId)
        {
           

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("FindLocation?id=" + locationId); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                
                ViewBag.locations = JObject.Parse(readTask.Result); ;
            }
            ViewBag.locationId = locationId;
            return View();
        }

        public ActionResult EditLocation(string locationId)
        {
            string ID = locationId;
            string name = Request.Form["location-name"];
            JObject location = new JObject();
            location["LocationID"] = ID ;
            location["LocationName"] = name;
            var content = new StringContent(JsonConvert.SerializeObject(location), System.Text.Encoding.UTF8, "application/json");
            JObject id = new JObject();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage2 = client.PutAsync("EditLocation/" + locationId, content);
            responseMessage2.Wait();
            var result = responseMessage2.Result;
            return RedirectToAction("/Facility");
        }
        public ActionResult DeleteLocation(string locationId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.DeleteAsync("DeleteLocation?id=" + locationId); /////////////////// truyen bien 
            responseMessage.Wait();
            return RedirectToAction("/Facility");

        }
        public ActionResult AddRoomView()
        {

            List<JObject> cinemas = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetCinema"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinemas.Add(o);
                }
                ViewBag.cinemas = cinemas;
            }
            return View();
        }
        public ActionResult AddRoom()
        {
            string ID = "ROOM";
            string name = Request.Form["room-name"];
            string CinemaID = Request.Form["discount_stt"];
            string ScreenType = Request.Form["room-screentype"];

            JObject room = new JObject();
            room["RoomID"] = ID;
            room["Roomname"] = name;
            room["CinemaID"] = CinemaID;
            room["ScreenType"] = ScreenType;

            var content = new StringContent(JsonConvert.SerializeObject(room), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage1 = client.PostAsync("AddRoom", content);
            responseMessage1.Wait();
            var result1 = responseMessage1.Result;
            return RedirectToAction("/Facility");
        }
        public ActionResult EditRoomView(string roomId)
        {
            List<JObject> cinemas = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetCinema"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    cinemas.Add(o);
                }
                
            }
            List<JObject> rooms = new List<JObject>(9999);

            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client1.DefaultRequestHeaders.Accept.Clear();
            client1.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage1 = client.GetAsync("FindRoom?id" + roomId); // 
            responseMessage.Wait();
            var result1 = responseMessage.Result;
            if (result1.IsSuccessStatusCode)
            {
                var readTask = result1.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    rooms.Add(o);
                }
               
            }
            ViewBag.rooms = rooms;
            ViewBag.cinemas = cinemas;
            ViewBag.roomId = roomId;
            return View();
        }

        public ActionResult EditRoom(string roomId)
        {
            string ID = roomId;
            string name = Request.Form["room-name"];
            string CinemaID = Request.Form["discount_stt"];
            string ScreenType = Request.Form["room-screentype"];

            JObject room = new JObject();
            room["RoomID"] = ID;
            room["Roomname"] = name;
            room["CinemaID"] = CinemaID;
            room["ScreenType"] = ScreenType;
            var content = new StringContent(JsonConvert.SerializeObject(room), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.PutAsync("EditRoom/" + roomId, content);
            responseMessage.Wait();
            return RedirectToAction("/Facility");
        }
        public ActionResult DeleteRoom(string roomId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Facility/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.DeleteAsync("DeleteRoom?id=" + roomId); /////////////////// truyen bien 
            responseMessage.Wait();
            return RedirectToAction("/Facility");
        }
    }
}