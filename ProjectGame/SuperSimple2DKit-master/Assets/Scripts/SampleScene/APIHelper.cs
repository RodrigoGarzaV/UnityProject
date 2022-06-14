using UnityEngine;
using System.Net;
using System.IO;
using System;
using System.Text;


public static class APIHelper
{

    // public static Applicante GetNewName(string Email)
    // {
    //     HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:5001/api/APIMySQLController/" + Email);
    //     HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    //     StreamReader reader = new StreamReader(response.GetResponseStream());
    //     string json = reader.ReadToEnd();
    //     return JsonUtility.FromJson<Applicante>(json);
        
    // }

    // public static void PostScore(int id, int score, int vacancy)
    // {   
    //     //HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("https://localhost:44307");
    //     https://localhost:5001/api/entrevista?scoreEntrevista=1300&idUsuario=1
    //     HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create("https://localhost:44307/id=" + id + "&score=" + score + "&vacancy=" + vacancy);

    //     var postData = "id=" + Uri.EscapeDataString(id.ToString());
    //     postData += "&score=" + Uri.EscapeDataString(score.ToString());
    //     postData += "&vacancy=" + Uri.EscapeDataString(vacancy.ToString());

    //     var data = Encoding.ASCII.GetBytes(postData);

    //     request2.Method = "POST";
    //     request2.ContentType = "application/x-www-form-urlencoded";
    //     request2.ContentLength = data.Length;

    //     using (var stream = request2.GetRequestStream())
    //     {
    //         stream.Write(data, 0, data.Length);
    //     }

    //     var response2 = (HttpWebResponse)request2.GetResponse();
        
    // }
}
