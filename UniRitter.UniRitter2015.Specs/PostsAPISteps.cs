using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UniRitter.UniRitter2015.Models;
using UniRitter.UniRitter2015.Services.Implementation;
using UniRitter.UniRitter2015.Support;
using UniRitter.UniRitter2015.Specs.Models;

namespace UniRitter.UniRitter2015.Specs
{
    [Binding]
    public class PostsAPISteps : BaseAPISteps
    {
      
        [Given(@"an API populated with the following posts")]
        public void GivenAnAPIPopulatedWithTheFollowingPosts(Table table)
        {
            CommonAPISteps.backgroundData = table.CreateSet<Post>();
            //var mongoRepo = new MongoRepository<PersonModel>(new ApiConfig());
            //mongoRepo.Upsert(table.CreateSet<PersonModel>());
            var repo = new InMemoryRepository<PostModel>();
            foreach (var entry in table.CreateSet<PostModel>())
            {
                repo.Add(entry);
            }
        }

        [Given(@"a post resource as described below:")]
        public void GivenAPostResourceAsDescribedBelow(Table table)
        {

            CommonAPISteps.postedData = new Post();
            table.FillInstance((Post)CommonAPISteps.postedData);
        }

        [When(@"I post it to the /posts API endpoint")]
        public void WhenIPostItToThePostsAPIEndpoint()
        {
            CommonAPISteps.response = client.PostAsJsonAsync("posts", (Post)CommonAPISteps.postedData).Result;
        }

        [When(@"I post the following data to the /posts API endpoint: (.+)")]
        public void WhenIPostTheFollowingDataToThePostsAPIEndpoint(string jsonData)
        {
            CommonAPISteps.postedData = JsonConvert.DeserializeObject<Post>(jsonData);
            CommonAPISteps.response = client.PostAsJsonAsync("posts", (Post)CommonAPISteps.postedData).Result;
        }

        
    }

}
