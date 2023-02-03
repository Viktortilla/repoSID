using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RestApiManager : MonoBehaviour
{
    [SerializeField]
    private RawImage YourRawImage;
    [SerializeField]
    private RawImage YourRawImage2;
    [SerializeField]
    private RawImage YourRawImage3;
    [SerializeField]
    private RawImage YourRawImage4;
    [SerializeField]
    private RawImage YourRawImage5;
    //rym
    [SerializeField]
    private RawImage YourRawImage6;
    [SerializeField]
    private RawImage YourRawImage7;
    [SerializeField]
    private RawImage YourRawImage8;
    [SerializeField]
    private RawImage YourRawImage9;
    [SerializeField]
    private RawImage YourRawImage10;
    
    private int[] DeskID;

    [SerializeField]
    private int UserId =1;
/*
    public void GetCharactersClick()
    {
        StartCoroutine(GetCharacters()); //ejecutar corrutina al dar al boton
    }
    */
    public void GetUsersClick()
    {
        StartCoroutine(GetPlayerInfo()); //ejecutar corrutina al dar al boton
    }
    IEnumerator GetPlayerInfo()
    {
        UnityWebRequest
            www = UnityWebRequest.Get("https://my-json-server.typicode.com/Viktortilla/JsonServerVictor/users/"+UserId);
        yield return www.Send();
        UnityWebRequest
            www2 = UnityWebRequest.Get("https://rickandmortyapi.com/api/character/"); //aqui se coloca la ruta api
        yield return www2.Send();

        if (www.isNetworkError) //revisa si hay un error, esta obsoleto
        {
            Debug.Log("NETWORK ERROR" + www.error);
            
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string json = www.downloadHandler.text;
            if (www.responseCode == 200) 
            {
                UserJsonData user = JsonUtility.FromJson<UserJsonData>(json);
                Debug.Log(user.name);
                
                foreach (int cardId in user.deck)
                {
                    
                    Debug.Log(cardId);
                    
                }

                DeskID = user.deck;
                StartCoroutine(DownloadImagex(user.cards[user.deck[0]],YourRawImage));
                StartCoroutine(DownloadImagex(user.cards[user.deck[1]],YourRawImage2));
                StartCoroutine(DownloadImagex(user.cards[user.deck[2]],YourRawImage3));
                StartCoroutine(DownloadImagex(user.cards[user.deck[3]],YourRawImage4));
                StartCoroutine(DownloadImagex(user.cards[user.deck[4]],YourRawImage5));
            }
            else
            {
                string mensaje = "Status :" + www.responseCode;
                mensaje += "/ncontent-type:" + www.GetResponseHeader("content-type:");
                mensaje += "/nError :" + www.error;
                Debug.Log(mensaje);
            }
            
            

            if (www2.isNetworkError) //revisa si hay un error, esta obsoleto
            {
                Debug.Log("NETWORK ERROR" + www2.error);
            }
            else
            {
                Debug.Log(www2.downloadHandler.text);
                string json2 = www2.downloadHandler.text;
                if (www2.responseCode == 200)
                {
                    CharactersList characters = JsonUtility.FromJson<CharactersList>(json2);
                    Character personaje = JsonUtility.FromJson<Character>(json2);
                    Debug.Log(characters.info.count);

                Debug.Log(DeskID[0]);
                 StartCoroutine(DownloadImagex(characters.results[DeskID[0]].image, YourRawImage6));
                 StartCoroutine(DownloadImagex(characters.results[DeskID[1]].image, YourRawImage7));
                 StartCoroutine(DownloadImagex(characters.results[DeskID[2]].image, YourRawImage8));
                 StartCoroutine(DownloadImagex(characters.results[DeskID[3]].image, YourRawImage9));
                 StartCoroutine(DownloadImagex(characters.results[DeskID[4]].image, YourRawImage10));


                }
                else
                {
                    string mensaje = "Status :" + www2.responseCode;
                    mensaje += "/ncontent-type:" + www2.GetResponseHeader("content-type:");
                    mensaje += "/nError :" + www2.error;
                    Debug.Log(mensaje);
                }


            }
        }
    }

   IEnumerator GetCharacters()
    {
        UnityWebRequest
            www = UnityWebRequest.Get("https://rickandmortyapi.com/api/character/"); //aqui se coloca la ruta api
        yield return www.Send();

        if (www.isNetworkError) //revisa si hay un error, esta obsoleto
        {
            Debug.Log("NETWORK ERROR" + www.error);
        }
        else
        {
            //preguntar por aplication json es para auto describir
            //Debug.Log(www.GetResponseHeader("content-type"));//por si quieres ver un encabezado

            // Show results as text
            Debug.Log(www.downloadHandler.text);

            string json = www.downloadHandler.text;

            //preguntar cual es el codigo de respuesta (errores)
            if (www.responseCode == 200) //el valor 200 significa que tod0 salio bien
            {
                //transformar la informacion en texto o un objeto 
                CharactersList characters = JsonUtility.FromJson<CharactersList>(json);
                Character personaje = JsonUtility.FromJson<Character>(json);
                Debug.Log(characters.info.count);
                 
                 //pedir nombres en una lisata, solo saldran 20 por la proteccion de la api
                 /*
                foreach (Character character  in characters.results)
                {
                    Debug.Log("Name:"+character.name);
                    //por la ciencia 
                    Debug.Log("image:"+character.image);//ruta de la imagen
                    //pediremos solo la primera imagen 
                    StartCoroutine(DownloadImage(character.image));
                    //break;
                }
                */
                 
                 Debug.Log(DeskID);
                 //StartCoroutine(DownloadImagex(characters.results[DeskID[0]].image,YourRawImage));
                 //StartCoroutine(DownloadImagex(characters.results[DeskID[1]].image,YourRawImage2));
                 //StartCoroutine(DownloadImagex(characters.results[DeskID[2]].image,YourRawImage3));
                 //StartCoroutine(DownloadImagex(characters.results[DeskID[3]].image,YourRawImage4));
                 //StartCoroutine(DownloadImagex(characters.results[DeskID[4]].image,YourRawImage5));

                 
            }
            else
            {
                string mensaje = "Status :" + www.responseCode;
                mensaje += "/ncontent-type:" + www.GetResponseHeader("content-type:");
                mensaje += "/nError :" + www.error;
                Debug.Log(mensaje);
            }


        }
    }
    
    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        
        //control de errores
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            YourRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
    IEnumerator DownloadImagex(string MediaUrl,RawImage YourRawImagex)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        
        //control de errores
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            YourRawImagex.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}

public class UserJsonData
{
    public int id;
    public string name;
    public int[] deck;
    public string[] cards;
}

[System.Serializable]
public class CharactersList
{
    public CharactersListInfo info;//este nombre no puede cambiarse
    public List<Character> results;//este nombre no puede cambiarse
}
[System.Serializable]
public class CharactersListInfo
{
    public int count;
    public int pages;
    public string next;
    public string prev;
}
[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public string species;
    public string image;
}
/*
IEnumerator GetCharacters()
    {
        UnityWebRequest
            www = UnityWebRequest.Get("https://rickandmortyapi.com/api/character/"); //aqui se coloca la ruta api
        yield return www.Send();

        if (www.isNetworkError) //revisa si hay un error, esta obsoleto
        {
            Debug.Log("NETWORK ERROR" + www.error);
        }
        else
        {
            //preguntar por aplication json es para auto describir
            //Debug.Log(www.GetResponseHeader("content-type"));//por si quieres ver un encabezado

            // Show results as text
            Debug.Log(www.downloadHandler.text);

            string json = www.downloadHandler.text;

            //preguntar cual es el codigo de respuesta (errores)
            if (www.responseCode == 200) //el valor 200 significa que tod0 salio bien
            {
                //transformar la informacion en texto o un objeto 
                CharactersList characters = JsonUtility.FromJson<CharactersList>(json);
                Debug.Log(characters.info.count);
                 
                 //pedir nombres en una lisata, solo saldran 20 por la proteccion de la api
                foreach (Character character  in characters.results)
                {
                    Debug.Log("Name:"+character.name);
                    //por la ciencia 
                    Debug.Log("image:"+character.image);//ruta de la imagen
                    //pediremos solo la primera imagen 
                    StartCoroutine(DownloadImage(character.image));
                    //break;
                }
                
            }
            else
            {
                string mensaje = "Status :" + www.responseCode;
                mensaje += "/ncontent-type:" + www.GetResponseHeader("content-type:");
                mensaje += "/nError :" + www.error;
                Debug.Log(mensaje);
            }


        }
    }
*/