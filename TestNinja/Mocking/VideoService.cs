using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        /*
            3種DI途徑
        
            1. 傳參 : 之後改參數，用到這個方法的類都要改, hen煩 (除非整個類只有一個方法用的那個依賴，那就可以考慮)
            2. 屬性
            3. 建構函數  

            DI Framework 推薦
            1. Ninject
            2. Autofac
         
        */
        private IFileReader _fileReader;
        private IVideoRepository _videoRepository;

        //用這個寫法，可以避免原本不是使用建構函數DI的類，不用整個重改
        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository=null)
        {
            // fileReader == null 就用 new FileReader() 給值
            _fileReader = fileReader ?? new FileReader();
            //沒用DI 框架才需要做這個判斷，不然一般都會幫你處理好
            _videoRepository = videoRepository ?? new VideoRepository();
        }



        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos=_videoRepository.GetUnprocessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);

        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}