// ArtistAlbum Database Model
using System.Collections.Generic;
using System.Text;

namespace DBR.Test
{
    // ArtistAlbum Base Class
    public class ArtistAlbum
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }

    // ArtistAlbum List
    public class ArtistAlbums : List<ArtistAlbum>
    {
        // Constructor: casts generic objects as ArtistAlbum type     
        public ArtistAlbums(IReadOnlyList<object> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                Add((ArtistAlbum)objects[i]);
            }
        }

        // Return record results
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ArtistAlbum aa in this)
            {
                sb.Append(string.Format("{0}\t{1,-20}\n", aa.Name, aa.Title));
            }
            return sb.ToString();
        }
    }
}
