using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GettingFBCalendarUser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public class FBusers
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }
    }


    public sealed partial class MainPage : Page
    {

        private static MobileServiceCollection<FBusers, FBusers> items ;
        private IMobileServiceTable<FBusers> todoTable = App.MobileService.GetTable<FBusers>();

        public List<FBusers> lstUsers = new List<FBusers>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        public static int recordCounts;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await getAllRecords();

        abc:
            if (!(items.Count < 50))
            {
                await getAllRecords();

                goto abc;
            }
            

            //recordCounts = 0;
            //items = await todoTable.ToCollectionAsync(50);
            //recordCounts = 50;
            //getMoreRecords();

            //var abc = await todoTable.Where( r => r.Complete == true).LoadAllAsync

            //items = await todoTable.Where(r => r.Gender == "female").ToCollectionAsync();

            lstData.ItemsSource = lstUsers;

        }

        public async Task getAllRecords()
        {
            IMobileServiceTableQuery<FBusers> query = todoTable.Where( x => x.Gender == "female").Skip(recordCounts).Take(50);
            items = await query.ToCollectionAsync();

            var abc = items.ToList<FBusers>();
            foreach (var item in abc)
            {
                lstUsers.Add(item);
            }

            recordCounts = recordCounts + 50;
        }

        #region commented
        //public async static Task<List<T>> LoadAllAsync<T>(this IMobileServiceTableQuery<T> table, int bufferSize = 1000)
        //{
        //    var query = table.IncludeTotalCount();
        //    var results = await query.ToEnumerableAsync();
        //    long count = ((ITotalCountProvider)results).TotalCount;
        //    if (results != null && count > 0)
        //    {
        //        var updates = new List<T>();
        //        while (updates.Count < count)
        //        {

        //            var next = await query.Skip(updates.Count).Take(bufferSize).ToListAsync();
        //            updates.AddRange(next);
        //        }
        //        return updates;
        //    }

        //    return null;
        //}
        #endregion
    }
}
