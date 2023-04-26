﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Areas.Admin.Controllers
{
    public class BusinessController : Controller
    {
        // GET: Admin/Business
        public ActionResult Business()
        {
            //var ID = (Session["Role"].ToString());
            //if (ID == "4" || ID == "1")
            //{
                ViewBag.name = Session["AdminID"];

                List<JObject> payment = new List<JObject>(9999);
                List<JObject> ticketType = new List<JObject>(9999);
                List<JObject> service = new List<JObject>(9999);
                List<JObject> discount = new List<JObject>(9999);
                List<JObject> room = new List<JObject>(9999);
                List<JObject> bill = new List<JObject>(9999);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetTicketTypes"); // 
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        ticketType.Add(o);
                    }
                    ViewBag.ticketType = ticketType;
                }
                //////////////////////
                responseMessage = client.GetAsync("GetServices"); // 
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        service.Add(o);
                    }
                    ViewBag.service = service;
                }

                responseMessage = client.GetAsync("GetDiscount");
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        discount.Add(o);
                    }
                    ViewBag.discount = discount;
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
                        room.Add(o);
                    }
                    ViewBag.room = room;
                }

                responseMessage = client.GetAsync("GetBill");
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        bill.Add(o);
                    }
                    ViewBag.bill = bill;
                }

                responseMessage = client.GetAsync("GetBill");
                responseMessage.Wait();
                result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    JArray listMovieJA = JArray.Parse(readTask.Result);
                    foreach (JObject o in listMovieJA.Children<JObject>())
                    {
                        payment.Add(o);
                    }
                    ViewBag.payment = payment.Count;
                }

                return View();
            //}
            //else
            //    return RedirectToAction("Homepage", "Admin");

        }

        public ActionResult AddTicketTypeView()
        {
            //var admins = db.Database.SqlQuery<AdminInfo>("exec GetAdminInfo").ToList();
            //var departments = db.DEPARTMENTs.ToList();
            //ViewBag.admins = admins;
            //ViewBag.departments = departments;
            List<JObject> admins = new List<JObject>(9999);
            List<JObject> departments = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetAdminInfo"); // 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    admins.Add(o);
                }
                ViewBag.admins = admins;
            }

            responseMessage = client.GetAsync("GetDepartments"); // 
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    departments.Add(o);
                }
                ViewBag.departments = departments;
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddTicketType()
        {
            string ID = Request.Form["ticket_type1"];
            string name = Request.Form["tickettype_name"];
            string price = Request.Form["tickettype_price"];
            JObject ticketType = new JObject();
            ticketType["TickeTypetName"] = name;
            ticketType["Price"] = price;
            var content = new StringContent(JsonConvert.SerializeObject(ticketType), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.PostAsync("AddTicketType", content); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            return RedirectToAction("/Business");
            

        }
        [HttpGet]
        public ActionResult EditTicketTypeView(string id)
        {
            List<JObject> ticket = new List<JObject>(9999);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetTicketType?id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                
                ViewBag.ticket = JObject.Parse(readTask.Result); ;
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditTicketType(string id)
        {
            
            JObject ticketType = new JObject();
           
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage = client.GetAsync("GetTicketType?id=" + id);
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ticketType = JObject.Parse(readTask.Result);
            }
            string name = Request.Form["tickettype_name"];
            string price = Request.Form["tickettype_price"];
            ticketType["TicketTypeName"] = name;
            ticketType["Price"] = price;
            var content = new StringContent(JsonConvert.SerializeObject(ticketType), System.Text.Encoding.UTF8, "application/json");
            var responseMessage1 = client.PutAsync("EditTicket/" + id, content); /////////////////// truyen bien 
            responseMessage.Wait();
            var result2 = responseMessage1.Result;
            return RedirectToAction("/Business");
        }
        public ActionResult DeleteTicketType(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.DeleteAsync("DeleteTicketType?id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            return RedirectToAction("/Business");
        }

        public ActionResult AddServiceView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddService()
        {
            string ID = Request.Form["serviceID"];
            string name = Request.Form["service_name"];
            string price = Request.Form["service_price"];
            JObject service = new JObject();
            service["ServiceID"] = ID;
            service["ServiceName"] = name;
            service["ServicePrice"] = price;
            var content = new StringContent(JsonConvert.SerializeObject(service), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage1 = client.PostAsync("AddService", content);
            responseMessage1.Wait();
            var result1 = responseMessage1.Result;
            return RedirectToAction("/Business");
        }
        [HttpGet]
        public ActionResult EditServiceView(string id)
        {

            Object service = new Object();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetService?id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                ViewBag.service = JObject.Parse(readTask.Result);
            }
            ViewBag.ServiceID = id;
            return View();
        }
        //[HttpPost]
        //public ActionResult EditService(string id)
        //{
        //    SERVICE s = new SERVICE();
        //    string name = Request.Form["service_name"];
        //    string price = Request.Form["service_price"];
        //    s.ServiceName = name;
        //    s.ServicePrice = price;
        //    var request = new RestRequest($"api/Business/PutService/{id}", Method.Put).AddObject(s);
        //    _client.Execute(request);
        //    return RedirectToAction("/Business");
        //}
        [HttpPost]
        public ActionResult EditService(string id)
        
            {
                JObject service = new JObject();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.GetAsync("GetService?id=" + id);
                responseMessage.Wait();
                var result = responseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    service = JObject.Parse(readTask.Result);
                }
                string name = Request.Form["service_name"];
                string price = Request.Form["service_price"];
                service["ServiceName"] = name;
                service["ServicePrice"] = price;
                var content = new StringContent(JsonConvert.SerializeObject(service), System.Text.Encoding.UTF8, "application/json");
                var responseMessage1 = client.PutAsync("EditService/" + id, content);
                responseMessage1.Wait();
                var result1 = responseMessage.Result;
                return RedirectToAction("/Business");
            }
            public ActionResult DeleteService(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.DeleteAsync("DeleteService?id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            return RedirectToAction("/Business");
        }

        public ActionResult AddDiscountView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDiscount()
        {
            string ID = Request.Form["discount_ID"];
            string t = Request.Form["discount_t"];
            string stt = Request.Form["discount_stt"];
            JObject discount = new JObject();
            discount["CodeID"] = ID;
            discount["DiscountNumber"] = t;
            discount["State"] = stt;
            var content = new StringContent(JsonConvert.SerializeObject(discount), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage1 = client.PostAsync("AddDiscount?id=", content);
            responseMessage1.Wait();
            var result1 = responseMessage1.Result;
            return RedirectToAction("/Business");
        }

        
        [HttpGet]
        public ActionResult EditDiscountView(string id)
        {
            ViewBag.DiscountID = id;
            JObject discount = new  JObject();
            List<JObject> discountTotal = new List<JObject>(9999);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.GetAsync("GetDiscount?id=" + id);  
            responseMessage.Wait();
            var result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                discount = JObject.Parse(readTask.Result);
            }
            ViewBag.discount = discount;
            HttpClient client2 = new HttpClient();
            client2.BaseAddress = new Uri("http://localhost:8085/api/Admin/"); // ???
            client2.DefaultRequestHeaders.Accept.Clear();
            client2.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            responseMessage = client.GetAsync("GetDiscount");
            responseMessage.Wait();
            result = responseMessage.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                JArray listMovieJA = JArray.Parse(readTask.Result);
                foreach (JObject o in listMovieJA.Children<JObject>())
                {
                    discountTotal.Add(o);
                }
                ViewBag.discountTotal = discountTotal;
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditDiscount(string id)
        {
            string ID = Request.Form["discount_ID"];
            string t = Request.Form["discount_t"];
            string stt = Request.Form["discount_stt"];
            JObject discount = new JObject();
            discount["CodeID"] = ID;
            discount["DiscountNumber"] = t;
            discount["State"] = stt;
            var content = new StringContent(JsonConvert.SerializeObject(discount), System.Text.Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var responseMessage1 = client.PutAsync("EditDiscount?id=", content);
            responseMessage1.Wait();
            var result1 = responseMessage1.Result;
            return RedirectToAction("/Business");
        }
        public ActionResult DeleteDiscount(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8085/api/Business/"); // ???
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responseMessage = client.DeleteAsync("DeleteDiscount?id=" + id); /////////////////// truyen bien 
            responseMessage.Wait();
            return RedirectToAction("/Business");
        }

    }
}