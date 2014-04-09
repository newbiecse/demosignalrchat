using DemoSignalRChat.Logic.Travel;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoSignalRChat.Controllers
{
    public class PlanController : Controller
    {
        //
        // GET: /Plan/
        public ActionResult Index()
        {
            string[] listCities = new string[] { "Ho Chi Minh", "Bac Ninh", "Hanoi", "Da Nang" };

            int SIZE = listCities.Length;

            List<int> distances = new List<int>();

            for (int i = 0; i < SIZE - 1; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    // Transit directions
                    var transitDirectionRequest = new DirectionsRequest
                    {
                        Origin = listCities[i] + " City, Vietnam",
                        Destination = listCities[j] + " City, Vietnam",
                        TravelMode = TravelMode.Driving,
                        OptimizeWaypoints = true
                    };

                    DirectionsResponse transitDirections = GoogleMaps.Directions.Query(transitDirectionRequest);

                    distances.Add(transitDirections.Routes.First().Legs.First().Distance.Value);
                }
            }

            int[,] cities = new int[SIZE, SIZE];

            int d = 0;
            for (int i = 0; i < SIZE - 1; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    cities[i, j] = distances[d];
                    cities[j, i] = distances[d];
                    d++;
                }
            }

            var oderedVisit = TSPAlgorithm.GTS(cities, SIZE);

            int x = 9;

            return View();
        }
	}
}