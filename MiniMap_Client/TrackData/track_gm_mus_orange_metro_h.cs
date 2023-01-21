using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace MiniMap_Client.TrackData
{
    class track_gm_mus_orange_metro_h
    {
        public static Point DepotOffset = new Point(202.5, 240);
        public static Point OrangeOffset = new Point(202.5, 240);

        public static object[,] TrackDataLines = new object[108,6]{
            //Depot
            {new Point(5,100),new Point(5,25),DepotOffset,null,"Depot 6",Brushes.LimeGreen},
            {new Point(20,100),new Point(20,25),DepotOffset,null,"Depot 5",null},
            {new Point(35,100),new Point(35,25),DepotOffset,null,"Depot 4",null},
            {new Point(50,100),new Point(50,25),DepotOffset,null,"Depot 3",null},
            {new Point(65,100),new Point(65,25),DepotOffset,null,"Depot 2",null},
            {new Point(80,100),new Point(80,25),DepotOffset,null,"Depot 1",null},
            {new Point(0,100),new Point(85,100),DepotOffset,null,"Depot",null},

            //{new Point(5,55),new Point(5,25),DepotOffset,null,"Depot",null},
            //{new Point(20,55),new Point(20,25),DepotOffset,null,"Depot",null},
            //{new Point(35,55),new Point(35,25),DepotOffset,null,"Depot",null},
            //{new Point(50,55),new Point(50,25),DepotOffset,null,"Depot",null},
            //{new Point(65,55),new Point(65,25),DepotOffset,null,"Depot",null},
            //{new Point(80,55),new Point(80,25),DepotOffset,null,"Depot",null},

            {new Point(5,20),new Point(15,0),DepotOffset,null,"Depot",Brushes.LimeGreen},
            {new Point(20,20),new Point(20,0),DepotOffset,null,"Depot",null},
            {new Point(35,20),new Point(35,0),DepotOffset,null,"Depot",null},
            {new Point(50,20),new Point(50,0),DepotOffset,null,"Depot",null},
            {new Point(65,20),new Point(65,0),DepotOffset,null,"Depot",null},
            {new Point(80,20),new Point(70,0),DepotOffset,null,"Depot",null},

            {new Point(20,-5),new Point(30,-25),DepotOffset,null,"Depot",Brushes.LimeGreen},
            {new Point(35,-5),new Point(35,-25),DepotOffset,null,"Depot",null},
            {new Point(50,-5),new Point(50,-25),DepotOffset,null,"Depot",null},
            {new Point(65,-5),new Point(55,-25),DepotOffset,null,"Depot",null},

            {new Point(35,-30),new Point(35,-50),DepotOffset,null,"Depot",Brushes.LimeGreen},
            {new Point(50,-30),new Point(50,-50),DepotOffset,null,"Depot",null},

            {new Point(35,-55),new Point(35,-85),DepotOffset,null,"Depot",null},
            {new Point(40,-55),new Point(45,-85),DepotOffset,null,"Depot",Brushes.LimeGreen},
            {new Point(45,-55),new Point(40,-85),DepotOffset,null,"Depot",null},
            {new Point(50,-55),new Point(50,-85),DepotOffset,null,"Depot",null},

            {new Point(35,-90),new Point(35,-120),DepotOffset,null,"P1-D5",null},
            {new Point(50,-90),new Point(50,-120),DepotOffset,null,"E2-D2",Brushes.LimeGreen},
            {new Point(55,-100),new Point(65,-120),DepotOffset,null,"Dead End",null},
            {new Point(65,-125),new Point(65,-140),DepotOffset,null,"Dead End",Brushes.Red},
            {new Point(60,-140),new Point(70,-140),DepotOffset,null,"Dead End",Brushes.Red},

            {new Point(35,-125),new Point(35,-155),DepotOffset,null,"D5-D3",null},
            {new Point(50,-125),new Point(50,-155),DepotOffset,null,"D2-D4",null},

            {new Point(35,-160),new Point(50,-190),DepotOffset,null,"D3-D1",null},
            {new Point(50,-160),new Point(65,-190),DepotOffset,null,"D4-D6",null},
            {new Point(53,-195),new Point(65,-207.5),DepotOffset,null,"D1-D",null},

            //To WHE
            {new Point(-180,-187.5),new Point(-150,-182.5),OrangeOffset,null,"1114-1112",null},
            {new Point(-145,-182.5),new Point(-115,-182.5),OrangeOffset,null,"1112-1110",null},
            {new Point(-110,-182.5),new Point(-80,-182.5),OrangeOffset,null,"1110-1108",null},
            {new Point(-75,-182.5),new Point(-45,-182.5),OrangeOffset,null,"1108-1106",null},
            {new Point(-40,-182.5),new Point(-10,-182.5),OrangeOffset,null,"1106-1104",null},
            {new Point(-5,-182.5),new Point(25,-182.5),OrangeOffset,null,"1104-VH1102",Brushes.YellowGreen}, //Station WHE

            //Wallace Breen turn(depot) to Wallace Breen Station
            {new Point(70,-207.5),new Point(100,-207.5),OrangeOffset,null,"D-VH3",null},
            {new Point(70,-192.5),new Point(100,-192.5),OrangeOffset,null,"D6-VH2",null},

            {new Point(105,-207.5),new Point(135,-207.5),OrangeOffset,null,"VH3-VH197",null},
            {new Point(105,-202.5),new Point(135,-197.5),OrangeOffset,null,"VH5-VB",null},
            {new Point(105,-197.5),new Point(135,-202.5),OrangeOffset,null,"VH4-VH197",null},
            {new Point(105,-192.5),new Point(135,-192.5),OrangeOffset,null,"VH2-198",null},

            {new Point(140,-207.5),new Point(170,-212.5),OrangeOffset,null,"VH3-VH197",null},
            {new Point(140,-192.5),new Point(170,-187.5),OrangeOffset,null,"VH2-198",null},

            //To Wallace Breen
            {new Point(30,-182.5),new Point(150,-182.5),OrangeOffset,null,"VH1102-VH1100",null},
            {new Point(155,-182.5),new Point(185,-182.5),OrangeOffset,null,"VH1100-198",null},
            {new Point(190,-182.5),new Point(220,-187.5),OrangeOffset,null,"198-196",null},
            {new Point(225,-187.5),new Point(255,-187.5),OrangeOffset,null,"196-194",null},
            {new Point(260,-187.5),new Point(290,-192.5),OrangeOffset,null,"194-192",null},
            {new Point(295,-192.5),new Point(325,-192.5),OrangeOffset,null,"192-190",null},
            {new Point(330,-192.5),new Point(360,-192.5),OrangeOffset,null,"190-188",null},
            {new Point(365,-192.5),new Point(395,-192.5),OrangeOffset,null,"188-186",null},
            {new Point(400,-192.5),new Point(430,-192.5),OrangeOffset,null,"186-184",Brushes.YellowGreen}, //Station Wallace Breen

            //To GCFS
            {new Point(435,-192.5),new Point(465,-192.5),OrangeOffset,null,"184-182",null},
            {new Point(470,-192.5),new Point(500,-192.5),OrangeOffset,null,"182-180",null},
            {new Point(505,-192.5),new Point(535,-192.5),OrangeOffset,null,"180-178",null},
            {new Point(540,-192.5),new Point(570,-192.5),OrangeOffset,null,"178-176",null},
            {new Point(575,-192.5),new Point(605,-192.5),OrangeOffset,null,"176-174",null},
            {new Point(610,-192.5),new Point(640,-192.5),OrangeOffset,null,"174-172",null},
            {new Point(645,-192.5),new Point(675,-192.5),OrangeOffset,null,"172-170",null},
            {new Point(680,-192.5),new Point(710,-192.5),OrangeOffset,null,"170-GS168",Brushes.YellowGreen}, //Station GCFS

            //To Park
            {new Point(715,-192.5),new Point(745,-192.5),OrangeOffset,null,"GS168-GS166",null},
            {new Point(750,-192.5),new Point(780,-192.5),OrangeOffset,null,"GS166-GS164",null},
            {new Point(785,-192.5),new Point(815,-192.5),OrangeOffset,null,"GS164-162",null},
            {new Point(820,-192.5),new Point(850,-192.5),OrangeOffset,null,"162-160",null},
            {new Point(855,-192.5),new Point(885,-192.5),OrangeOffset,null,"160-158",null},
            {new Point(890,-192.5),new Point(920,-192.5),OrangeOffset,null,"158-156",null},
            {new Point(925,-192.5),new Point(955,-192.5),OrangeOffset,null,"156-154",null},
            {new Point(960,-192.5),new Point(990,-192.5),OrangeOffset,null,"154-152",null},
            {new Point(995,-192.5),new Point(1025,-192.5),OrangeOffset,null,"152-PR150",Brushes.YellowGreen}, //Station Park

            //From WHE
            {new Point(25,-217.5),new Point(-5,-217.5),OrangeOffset,null,"1103-1101",Brushes.YellowGreen}, //Station WHE
            {new Point(-10,-217.5),new Point(-40,-217.5),OrangeOffset,null,"1105-1103",null},
            {new Point(-45,-217.5),new Point(-75,-217.5),OrangeOffset,null,"1107-1105",null},
            {new Point(-80,-217.5),new Point(-110,-217.5),OrangeOffset,null,"1109-1107",null},
            {new Point(-115,-217.5),new Point(-145,-217.5),OrangeOffset,null,"1111-1109",null},
            {new Point(-150,-217.5),new Point(-180,-212.5),OrangeOffset,null,"1113-1111",null},
            {new Point(-185,-210.5),new Point(-185,-189.5),OrangeOffset,null,"1113-1114",Brushes.YellowGreen}, //Station GM Workers Station

            //From Wallace Breen
            {new Point(430,-207.5),new Point(400,-207.5),OrangeOffset,null,"183-181",Brushes.YellowGreen}, //Station Wallace Breen
            {new Point(395,-207.5),new Point(370,-207.5),OrangeOffset,null,"185-183",null},
            {new Point(365,-207.5),new Point(340,-207.5),OrangeOffset,null,"187-185",null},
            {new Point(335,-207.5),new Point(310,-207.5),OrangeOffset,null,"189-187",null},
            {new Point(305,-207.5),new Point(275,-212.5),OrangeOffset,null,"191-189",null},
            {new Point(270,-212.5),new Point(245,-212.5),OrangeOffset,null,"193-191",null},
            {new Point(240,-212.5),new Point(210,-217.5),OrangeOffset,null,"195-193",null},
            {new Point(205,-217.5),new Point(180,-217.5),OrangeOffset,null,"VH197-195",null},
            {new Point(175,-217.5),new Point(65,-217.5),OrangeOffset,null,"199-VH197",null},
            {new Point(60,-217.5),new Point(30,-217.5),OrangeOffset,null,"1101-199",null},

            //From GCFS
            {new Point(710,-207.5),new Point(680,-207.5),OrangeOffset,null,"167-GS165",Brushes.YellowGreen}, //Station GCFS
            {new Point(675,-207.5),new Point(645,-207.5),OrangeOffset,null,"169-167",null},
            {new Point(640,-207.5),new Point(610,-207.5),OrangeOffset,null,"171-169",null},
            {new Point(605,-207.5),new Point(575,-207.5),OrangeOffset,null,"173-171",null},
            {new Point(570,-207.5),new Point(540,-207.5),OrangeOffset,null,"175-173",null},
            {new Point(535,-207.5),new Point(505,-207.5),OrangeOffset,null,"177-175",null},
            {new Point(500,-207.5),new Point(470,-207.5),OrangeOffset,null,"179-177",null},
            {new Point(465,-207.5),new Point(435,-207.5),OrangeOffset,null,"181-179",null},

            //From Park
            {new Point(1025,-207.5),new Point(995,-207.5),OrangeOffset,null,"147-PR145",Brushes.YellowGreen}, //Station Park
            {new Point(990,-207.5),new Point(960,-207.5),OrangeOffset,null,"149-147",null},
            {new Point(955,-207.5),new Point(925,-207.5),OrangeOffset,null,"151-149",null},
            {new Point(920,-207.5),new Point(895,-207.5),OrangeOffset,null,"153-151",null},
            {new Point(890,-207.5),new Point(865,-207.5),OrangeOffset,null,"155-153",null},
            {new Point(860,-207.5),new Point(835,-207.5),OrangeOffset,null,"157-155",null},
            {new Point(830,-207.5),new Point(805,-207.5),OrangeOffset,null,"GS159-157",null},
            {new Point(800,-207.5),new Point(775,-207.5),OrangeOffset,null,"GS161-GS159",null},
            {new Point(770,-207.5),new Point(745,-207.5),OrangeOffset,null,"GS163-GS161",null},
            {new Point(740,-207.5),new Point(715,-207.5),OrangeOffset,null,"GS165-GS163",null}
        };

        public static object[,] StationDataLines = new object[5,4]{
            {new Point(-185,-213.5),OrangeOffset,null,"GM Workers Station"},
            {new Point(-7.5,-213.5),OrangeOffset,null,"VHE"},
            {new Point(375,-213.5),OrangeOffset,null,"Wallace Breen"},
            {new Point(675,-213.5),OrangeOffset,null,"GCFS"},
            {new Point(994,-213.5),OrangeOffset,null,"Park"}
        };

        public static object[,] MapSignalsDataLines = new object[,]{

        };
    }
}