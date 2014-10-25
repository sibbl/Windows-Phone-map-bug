using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;

namespace MapBugDemo
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Map.Center = new Geopoint(new BasicGeoposition
            {
                Latitude = 52.516411,
                Longitude = 13.379102
            });
            Map.ZoomLevel = 16;

            //outer rect
            var positionsExterior = new List<BasicGeoposition>
            {
                new BasicGeoposition
                {
                    Latitude = 52.517762,
                    Longitude = 13.377049
                },
                new BasicGeoposition
                {
                    Latitude = 52.518079,
                    Longitude = 13.380335
                },
                new BasicGeoposition
                {
                    Latitude = 52.515150,
                    Longitude = 13.381543
                },
                new BasicGeoposition
                {
                    Latitude = 52.514568,
                    Longitude = 13.377386
                }
            };

            //inner rect
            var positionsInterior = new List<BasicGeoposition>
            {
                new BasicGeoposition
                {
                    Latitude = 52.516770, 
                    Longitude = 13.378183
                },
                new BasicGeoposition
                {
                    Latitude = 52.516870, 
                    Longitude = 13.379421
                },
                new BasicGeoposition
                {
                    Latitude = 52.515949, 
                    Longitude = 13.379562
                },
                new BasicGeoposition
                {
                    Latitude = 52.515870, 
                    Longitude = 13.378348
                }
            };

            //close both rings
            positionsInterior.Add(positionsInterior[0]);
            positionsExterior.Add(positionsExterior[0]);

            var lineInt = new MapPolyline()
            {
                Path = new Geopath(positionsInterior),
                StrokeColor = Colors.Blue,
                StrokeThickness = 4
            };
            Map.MapElements.Add(lineInt);

            var lineExt = new MapPolyline()
            {
                Path = new Geopath(positionsExterior),
                StrokeColor = Colors.Blue,
                StrokeThickness = 4
            };
            Map.MapElements.Add(lineExt);


            //the base is the outer ring
            var complexPolygon = positionsExterior.ToList();
            //then we add the inner ring
            complexPolygon.AddRange(positionsInterior);
            //finally add the first/last point of the outer ring again to close the path
            complexPolygon.Add(positionsExterior[0]);

            var fill = new MapPolygon
            {
                Path = new Geopath(complexPolygon),
                FillColor = Colors.Red
            };
            Map.MapElements.Add(fill);
        }
    }
}
