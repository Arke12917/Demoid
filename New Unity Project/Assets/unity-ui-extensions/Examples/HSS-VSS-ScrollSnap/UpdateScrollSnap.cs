namespace UnityEngine.UI.Extensions
{
	using System.IO;
    public class UpdateScrollSnap : MonoBehaviour
    {

        public UnityEngine.UI.Extensions.HorizontalScrollSnap HSS;
        public UnityEngine.UI.Extensions.VerticalScrollSnap VSS;
        public GameObject HorizontalPagePrefab;
        public GameObject VerticalPagePrefab;
        public UnityEngine.UI.InputField JumpPage;
		public int pagechanged;
		public static ScrollSnapBase SSB;
		public static VerticalScrollSnap VSNP;
		public songnamechange SNC;
		public string songtoload;
		public prefabat pfb;
		public Text difficulty;
		public Text sc0re;
		public GameObject AC;
		public GameObject FC;

		void Awake(){

		}
		void update(){
			
		}
        public void AddButton()
        {
            if (HSS)
            {
                var newHSSPage = GameObject.Instantiate(HorizontalPagePrefab);
                HSS.AddChild(newHSSPage);
            }
            if (VSS)
            {
                var newVSSPage = GameObject.Instantiate(VerticalPagePrefab);
                VSS.AddChild(newVSSPage);
            }
        }

        public void RemoveButton()
        {
            GameObject removed, removed2;
            if (HSS)
            {
                HSS.RemoveChild(HSS.CurrentPage, out removed);
                removed.SetActive(false);
            }
            if (VSS)
            {
                VSS.RemoveChild(VSS.CurrentPage, out removed2);
                removed2.SetActive(false);
            }
        }

        public void JumpToPage()
        {
            int jumpPage = int.Parse(JumpPage.text);
            if (HSS)
            {
                HSS.GoToScreen(jumpPage);
            }
            if (VSS)
            {
                VSS.GoToScreen(jumpPage);
            }
        }

        public void SelectionStartChange()
        {
            Debug.Log("Scroll Snap change started");
        }
        public void SelectionEndChange()
        {
            Debug.Log("Scroll Snap change finished");
        }
        public void PageChange(int page)
        {
            Debug.Log(string.Format("Scroll Snap page changed to {0}", page));
			pagechanged = page;
			stopsongs ();

		
        }

        public void RemoveAll()
        {
            GameObject[] children;
            HSS.RemoveAllChildren(out children);
            VSS.RemoveAllChildren(out children);
        }

        public void JumpToSelectedToggle(int page)
        {
            HSS.GoToScreen(page);
        }
		public void stopsongs()
		{
		var Songss = GameObject.FindGameObjectsWithTag("listedSN");
			foreach(GameObject Song in Songss){
				if (Song.GetComponent<songnamechange> ().thispagelol == pagechanged) {
					Song.transform.gameObject.tag="listedNS";
					songtoload = Song.gameObject.name;
					GameObject.FindGameObjectWithTag("listedNS").GetComponent<AudioSource>().Play();
					Song.transform.gameObject.tag="listedSN";
					Debug.Log ("the song has been started");
				
			

			

				} 
				else {
					var Songi = GameObject.FindGameObjectsWithTag("listedSN");
					foreach(GameObject Sung in Songi){
						if(Sung.GetComponent<songnamechange>().thispagelol != pagechanged){
							Sung.transform.gameObject.tag="Nope";
				    GameObject.FindGameObjectWithTag("Nope").GetComponent<AudioSource>().Stop();
					Sung.transform.gameObject.tag="listedSN";
					Debug.Log ("the song has been stopped");
					}
					//GetComponent<AudioSource> ().clip = songnamechange.musicplayerr.Stop;
				}
		}
    }
}
		public void bgalpha()
		{
			/*var bgs = GameObject.FindGameObjectsWithTag("State0");
			foreach(GameObject bga in bgs){
				if (bga.GetComponent<songnamechange> ().thispagelol == pagechanged) {
					bga.transform.gameObject.tag="State1";
					GameObject.FindGameObjectWithTag ("State1").GetComponent<songnamechange> ().StartCoroutine("tempBGcolor");
					bga.transform.gameObject.tag="State0";
					Debug.Log ("the bg has been changed");



				} 
				else {
					var bgi = GameObject.FindGameObjectsWithTag("State0");
					foreach(GameObject bgz in bgi){
						if(bgz.GetComponent<songnamechange>().thispagelol != pagechanged){
							bgz.transform.gameObject.tag="Nah";
							GameObject.FindGameObjectWithTag ("Nah").GetComponent<songnamechange>().StartCoroutine("tempBGcolor1");
							bgz.transform.gameObject.tag="State0";
							Debug.Log ("the bg has been deactivated");
						}
					}
				}
			}*/
		}
		public void setBG()
		{
			var bgs = GameObject.FindGameObjectsWithTag ("State0");
			foreach (GameObject bga in bgs) {
				if (bga.GetComponent<songnamechange> ().thispagelol == pagechanged) {
					bga.transform.gameObject.tag = "State1";
					GameObject.FindGameObjectWithTag ("State1").GetComponent<songnamechange> ().StartCoroutine ("chooseBG");
					bga.transform.gameObject.tag = "State0";
					Debug.Log ("the bg has been changed");
					GameObject.FindGameObjectWithTag ("YEABOI").GetComponent<prefabat> ().chngname ();
					//GameObject.FindGameObjectWithTag ("YEABOI").GetComponent<prefabat> ().CreatePrefab ();
				}
			}
		}
		public void changediff()
		{
			var Diff = GameObject.FindGameObjectsWithTag("Difint");
			foreach(GameObject dif in Diff){
				if (dif.GetComponent<DONTDESTRORYSP> ().difpage == pagechanged) {
					dif.transform.gameObject.tag="DifSet";
					difficulty.text=GameObject.FindGameObjectWithTag("DifSet").name;
					dif.transform.gameObject.tag="Difint";
					Debug.Log ("Difficulty has been changed");





				} 
				else {

				}
			}
		}
		public void FINDDIF(){
			GameObject.FindGameObjectWithTag ("LVLTXT").name = GameObject.FindGameObjectWithTag ("LVLTXT").GetComponent<Text> ().text;
			GameObject.FindGameObjectWithTag ("DIFFICON").GetComponent<changedifimage> ().changeimagehere ();
			GameObject.FindGameObjectWithTag ("YEABOI").GetComponent<Text>().text = GameObject.FindGameObjectWithTag ("LVLTXT").GetComponent<Text> ().text;
			GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().StartCoroutine ("SNG");
		}

		public void getsc0re(){
			ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
			sc0re.text = ZPlayerPrefs.GetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name).ToString ("F2") + "%";
			if (ZPlayerPrefs.GetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name + "AC") == 1f) {
				AC.SetActive (true);
				FC.SetActive (false);
			} else if (ZPlayerPrefs.GetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name + "FC") == 1f) {
				FC.SetActive (true);
				AC.SetActive (false);
			} else {
				AC.SetActive (false);
				FC.SetActive (false);
			}
			if (sc0re.text == null) {
				sc0re.text = "No Score Yet!";
			}


		}

}
}
