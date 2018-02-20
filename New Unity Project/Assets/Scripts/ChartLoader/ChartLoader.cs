using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using Prime31;
using DeemoChart;

public class ChartLoader : MonoBehaviour {

	static public D_Chart DeserializeChart (string chart) {
		D_Chart output;
		try {
			output = JsonConvert.DeserializeObject<D_Chart> (chart);
		} catch (Exception e) {
			//throw new Exception (e.ToString ());
			Debug.Log(e.ToString());
			output = DeserializeManual (chart);
		}
		if (output == null) {
			throw new ArgumentNullException ("Chart can't be Deserialize ?");
		}
		return output;
	}

	static D_Chart DeserializeManual (string json) {
		D_Chart ch = new D_Chart ();
		Dictionary<string, object> dictionary = json.dictionaryFromJson ();
		foreach (var kp in dictionary) {
			if (kp.Key == "speed") {
				ch.speed = Convert.ToDouble (kp.Value);
			} else if (kp.Key == "notes") {
				List<D_Note> lnotes = new List<D_Note> ();
				List<object> notes = (kp.Value.ToString()).listFromJson ();
				foreach (var n in notes) {
					Dictionary<string, object> main_props = n.ToString ().dictionaryFromJson ();
					D_Note n_ = new D_Note ();
					foreach (var props in main_props) {
						if (props.Key == "$id") {
							n_.id_ = props.Value.ToString();
						} else if (main_props.ContainsKey ("type") && props.Key == "type") {
							n_.type = Convert.ToInt32(props.Value);
						} else if (main_props.ContainsKey ("sounds") && props.Key == "sounds") {
							List<D_Sound> s = new List<D_Sound> ();
							List<object> soundss = props.Value.ToString ().listFromJson ();
							foreach (var sound in soundss) {
								D_Sound ss = new D_Sound ();
								Dictionary<string, object> so = sound.ToString ().dictionaryFromJson ();
								foreach (var s_va in so) {
									if (s_va.Key == "w") {
										ss.w = Convert.ToDouble(s_va.Value);
									} else if (s_va.Key == "d") {
										ss.d = Convert.ToDouble(s_va.Value);
									} else if (s_va.Key == "p") {
										ss.p = Convert.ToInt32(s_va.Value);
									} else if (s_va.Key == "v") {
										ss.v = Convert.ToInt32(s_va.Value);
									}
								}
								s.Add (ss);
							}
							n_.sounds = s;
						} else if (props.Key == "pos") {
							n_.pos = Convert.ToDouble(props.Value);
						} else if (props.Key == "size") {
							n_.size = Convert.ToDouble(props.Value);
						} else if (props.Key == "_time") {
							n_._time = Convert.ToDouble(props.Value);
						} else if (props.Key == "shift") {
							n_.shift = Convert.ToDouble(props.Value);
						} else if (props.Key == "time") {
							n_.time = Convert.ToDouble(props.Value);
						}
					}
					lnotes.Add (n_);
				}
				ch.notes = lnotes;
			} else if (kp.Key == "links") {
				List<D_Link> llinks = new List<D_Link> ();
				List<object> l1 = kp.Value.ToString ().listFromJson ();
				foreach (var sama in l1) {
					D_Link lll = new D_Link ();
					Dictionary<string, object> dl = sama.ToString ().dictionaryFromJson ();
					List<object> ref_d = dl ["notes"].ToString ().listFromJson ();
					List<D_Note2> l2 = new List<D_Note2> ();
					foreach (var ref_dd in ref_d) {
						D_Note2 note2 = new D_Note2 ();
						Dictionary<string, object> only1ref = ref_dd.ToString ().dictionaryFromJson ();
						note2.ref_ = only1ref ["$ref"].ToString();
						l2.Add (note2);
					}
					lll.notes = l2;
					llinks.Add (lll);
				}
				ch.links = llinks;
			}
		}
		if (ch != null) {
			Debug.Log ("Success");
			//Debug.Log (JsonConvert.SerializeObject (ch));
		}
		return ch;
	}

}

namespace DeemoChart {
	
	public class D_Sound
	{
		[JsonProperty("w")]
		public double? w { get; set; }

		[JsonProperty("d")]
		public double? d { get; set; }

		[JsonProperty("p")]
		public int? p { get; set; }

		[JsonProperty("v")]
		public int? v { get; set; }
	}

	public class D_Note
	{
		[JsonProperty("$id")]
		public string id_ { get; set; }

		[JsonProperty("type")]
		public int? type { get; set; }

		[JsonProperty("sounds")]
		public List<D_Sound> sounds { get; set; }

		[JsonProperty("pos")]
		public double pos { get; set; }

		[JsonProperty("size")]
		public double size { get; set; }

		[JsonProperty("_time")]
		public double _time { get; set; }

		[JsonProperty("shift")]
		public double? shift { get; set; }

		[JsonProperty("time")]
		public double? time { get; set; }
	}

	public class D_Note2
	{
		[JsonProperty("$ref")]
		public string ref_ { get; set; }
	}

	public class D_Link
	{
		[JsonProperty("notes")]
		public List<D_Note2> notes { get; set; }
	}

	public class D_Chart
	{
		[JsonProperty("speed")]
		public double speed { get; set; }

		[JsonProperty("notes")]
		public List<D_Note> notes { get; set; }

		[JsonProperty("links")]
		public List<D_Link> links { get; set; }
	}

}

#if UNITY_EDITOR
public class Testy : UnityEditor.EditorWindow {
	[UnityEditor.MenuItem("Tools/Test Chart-Deserialize")]
	static public void TestParseChart() {
		string path = "";
		try {
			path = UnityEditor.EditorUtility.OpenFilePanel("Open Chart File...", System.IO.Directory.GetCurrentDirectory(), "*");
			ChartLoader.DeserializeChart(System.IO.File.ReadAllText(path, System.Text.Encoding.ASCII));
		} catch (Exception e) {
			Debug.Log (e.ToString ());
		}
		Debug.Log ("Done");
	}
}

#endif