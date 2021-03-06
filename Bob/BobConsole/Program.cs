﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob;

namespace BobConsole {
	class Program {
		static void PrintGrandsireTriplesPlainCourse () {
			Console.WriteLine (Method.GetMethod ("Grandsire Triples").plain_course);
		}

		static void Print120OfPlainBobDoubles () {
			Console.WriteLine (Method.GetMethod ("Plain Bob Doubles").TouchFromCallList ("PPPB"));
		}

		static void PrintSpacer () {
			Console.WriteLine ("\n\n\n\n\n");
		}

		static void Print720OfBobMinor () {
			Console.WriteLine (Method.GetMethod ("Plain Bob Minor").TouchFromCallingPositions ("WsWWsWH").LeadHeadString ());
		}

		static void PrintHalfACourseOfCambridgeMajor () {
			Console.WriteLine (new Touch (
				Method.GetMethod ("Cambridge Surprise Major"),
				new MethodCall [] {new MethodCall (
					new Method ("X18", "Original", Stage.Major),
					new Touch.CallLocationCountDown (3),
					-2
				)}
			));
		}

		static void PrintSomeBobMinorAndDoublesSpliced () {
			Console.WriteLine (new Touch (
				new Method [] { Method.GetMethod ("Plain Bob Doubles"), Method.GetMethod ("Plain Bob Minor") }
			));
		}
		
		static void PrintACalledChangeTouch () {
			Console.WriteLine (new CalledChangeTouch (
				Stage.Doubles,
				new CalledChange [] {
					CalledChange.ByPlaceCalledUp (1),
					CalledChange.ByPlaceCalledUp (3),
					CalledChange.ByPlaceCalledUp (2)
				}
			));
		}

		static void PrintATouchWhichDoesntComeRound () {
			Console.WriteLine (Method.GetMethod ("Plain Bob Minor").TouchFromCallingPositions ("I"));
		}

		static void ComputeAnExtentOfStRemigiusBobDoubles () {
			string [] extents = Method.GetMethod ("St Remigius Bob Doubles").GenerateExtents ("MB");

			foreach (string s in extents) {
				Console.WriteLine (s);
			}
		}

		static void GenerateAnExtentOfLetsRingDelightMinor () {
			Method lets_ring = new Method ("56x56.14x56x16x12x16,12", "Let's Ring is a", Stage.Minor);

			string [] extents = lets_ring.GenerateExtents ("MB", 10, print: true);

			foreach (string s in extents) {
				Console.WriteLine (s);
			}
		}

		// Looks like this isn't possible
		static void GenerateAnExtentOfSaturnDoubles () {
			Method saturn = Method.GetMethod ("Saturn Doubles");

			saturn.SetLeadEndCalls ("3", "123");

			string [] extents = saturn.GenerateExtents ("MBS");

			foreach (string s in extents) {
				Console.WriteLine (s);
			}
		}

		static void DemonstrateSpeedBreakdown () {
			SpeedProfiler profiler = new SpeedProfiler (4);

			int n = 20;

			for (int x = 0; x < n; x++) {
				Touch t = Method.GetMethod ("Plain Bob Triples").TouchFromCallingPositions ("OHHH sWHHH WFHHH IH");
				// new Method ("56x56.14x56x16x12x16,12", "Let's Ring is a", Stage.Minor).TouchFromCallingPositions ("WHW");

				profiler.Profile ();

				Change [] c = t.changes;

				profiler.Profile ();

				Dictionary <int, int> d = t.change_repeat_frequencies;

				profiler.Profile ();

				string s = t.ToString ();

				profiler.Profile ();

				Console.Write (".");

				if ((x + 1) % 20 == 0) {
					Console.Write ("\n");

					if ((x + 1) % 100 == 0) {
						Console.Write ("\n");
					}
				}
			}
			
			profiler.Print (new string [] {
				"1. Creating Method and Touch objects",
				"2. Computing changes in the touch",
				"3. Running truth check",
				"4. Constructing string representation"
			}, ":\n >> ");
		}

		static void PrintTheCoursingOrderOfPB8 () {
			Console.WriteLine (Method.GetMethod ("Plain Bob Major").GetCoursingOrderString ());
		}

		static void CheckForBobOnlyCompositionsOfBobMinor () {
			Stopwatch stopwatch = Stopwatch.StartNew ();

			Console.WriteLine (Method.GetMethod ("Cambridge Surprise Minor").GenerateExtents ("MB", -1, 1));

			Console.WriteLine (stopwatch.Elapsed.TotalSeconds);
		}

		static void LRDMTests () {
			Method lets_ring = new Method ("56x56.14x56x16x12x16,12", "Let's Ring is a", Stage.Minor);

			while (true) {
				string input = Console.ReadLine ();

				Touch touch = lets_ring.TouchFromCallingPositions (input); // lets_ring.TouchFromCallList ("MMBMMBMBMM");

				Console.WriteLine (touch.LeadHeadString ());
			}
		}

		static void PB5and6Spliced () {
			Method d = Method.GetMethod ("Plain Bob Doubles");
			Method b = new Method ("56.16.56.16.56,1456", "Bobbed", Stage.Minor);
			Method m = Method.GetMethod ("Plain Bob Minor");
			Method B = new Method ("x16x16x16,14", "Bobbed", Stage.Minor);
			Method S = new Method ("x16x16x16,1234", "Singled", Stage.Minor);

			Touch touch = new Touch (
				new Method [] {
					d,d,m,m,m,m,
					d,m,b,
					d,d,d,d,m,m,m,m,
					b,m,

					d,d,m,m,m,m,
					d,m,b,
					d,d,d,d,m,m,m,m,
					b,B

					/*
					d,d,m,m,m,m,
					d,m,b,
					d,d,d,d,m,m,m,m,
					b,S,

					d,d,m,m,m,m,
					d,m,b,
					d,d,d,d,m,m,m,m,
					b,S,

					d,d,m,m,m,m,
					d,m,b,
					d,d,d,d,m,m,m,m,
					b,m
					*/
					/*
					b,m,
					d,d,m,m,m,m,
					d,m,b,m,m,
					d,d,d,d,m,S,

					b,m,
					d,d,m,m,m,m,
					d,m,b,m,m,
					d,d,d,d,m,S,

					b,m,
					d,d,m,m,m,m,
					d,m,b,m,m,
					d,d,d,d,m,B
					*/
				}
			) {
				rounds_checks = RoundsCheckLocations.OnlyLeadEnds
			};

			Console.WriteLine (touch.LeadHeadString ());
		}

		static void PrintTouchOfStedman () {
			Console.WriteLine (Method.GetMethod ("Stedman Doubles").TouchFromCallList ("MMMMS").LeadHeadString ());
		}

		static void Main (string [] args) {


			Console.ReadKey ();
		}
	}
}
