﻿ALTER TABLE [Users] ADD [RolesAsInt] [int] NOT NULL DEFAULT 0
INSERT INTO [__MigrationHistory] ([MigrationId], [CreatedOn], [Model], [ProductVersion]) VALUES ('201208020426592_UserRolesAsInt', '2012-08-02T04:27:12.546Z', 0x1F8B0800000000000400ECBD07601C499625262F6DCA7B7F4AF54AD7E074A10880601324D8904010ECC188CDE692EC1D69472329AB2A81CA6556655D661640CCED9DBCF7DE7BEFBDF7DE7BEFBDF7BA3B9D4E27F7DFFF3F5C6664016CF6CE4ADAC99E2180AAC81F3F7E7C1F3F22FEC7BFF71F7CFC7BBC5B94E9655E3745B5FCECA3DDF1CE4769BE9C56B36279F1D947EBF67CFBE0A3DFE3E8374E1E9FCE16EFD29F34EDF6D08EDE5C369F7D346FDBD5A3BB779BE93C5F64CD78514CEBAAA9CEDBF1B45ADCCD66D5DDBD9D9D83BBBB3B777302F111C14AD3C7AFD6CBB658E4FC07FD79522DA7F9AA5D67E517D52C2F1BFD9CBE79CD50D317D9226F56D934FFECA3A7599B7D941E9745467DBFCECBF3F74464E72110F9C876419D9C1232EDF59BEB55CE1D7DF6D1B3222F677E136AF47BE5D7C107F4D1CBBA5AE5757BFD2A3FD717CF661FA577C3F7EE765FB4AF79EFA06FFA6DD9DEDBFB287DB12ECB6C52D207E759D9E41FA5AB4F1FBD6EAB3AFF3C5FE675D6E6B39759DBE6F512EFE68CBBD2E0D1EAD3DB91E1E1DD9D3D90E16EB65C566DD6D2A4F610EFA0F9346FA675B192A682EFEBB62636F9287D56BCCB67CFF3E5453BB7387F91BD339FD0AF1FA55F2D0BE22A7AA9ADD7B93F46F97B73DFAF8B1FE4C70D91E7264A6D06735CE73455799DE7CDABFC17AD8B3AB7A47F5255659E2DDF1BE4B7B3E67971316F9B0F05745C96D5553E3B9EB6C5254DE8D719ED8BECB2B8E0B9EC92AFAC80E0ABBCE46F9B79B112D119E39BDF5FB83D7D56578B5715C0BB4F7FFF37597D910391AAF7D5EB6A5D4F3B583CBEEB6469A38401CE8F042C9CA536AB5BD26EF91BD28C0663F7F77BF2D3E972F6F5610D7292B2CAD7E124C32E114E324C765B2C3E87358862816F7E7FE62D0F09FB618F9BDD371FC4CC00F323661E52B52FF27CF6E18AD60348DEC279512F3E1CE68BAACDADE2FEE1193322F56C5DE6B32FAD21BDBD6476609D64E438DD08EA3658FDAC199EAF23A75D5DD197E0DB62F026CF16BBC328E8D71D1CF8D33812F2D5D7C1626F33167B512CF686B1D87B6F2C2CE33DB9DE30297EA3EEDCB8EF06A6C86BF0BED81956DE849CDFA6839BFB2A8E9AF77D0CB35BEB7A909EC28F49D3D6D9B455D9FA91EE0FD46AE67C8E1F9A567D9E5FE6E537AABC4ECAF524CA8A6081DF5FBE754C683FECB19FFBE67D45E22969E486093E84856BD1C1C47C11C7C67EFB41A2C024F811EBFFACB2FEEDBC001A1C450FEDCF7AD7B7660DCB973F628FC0C9BAB053744BEDD48D7CF2E52CAFBFD169EE2BD75BCF321480D8C32759932B4E463775A67E50C97D912DB30BCA2B4695DC574D5EB3B7D3F81ACE7DDA8BE8BCAF3E28A4039C1F31EFCFAE6EBB8D593F5D6445793C9BD579F3C38FD45ECEAB65FE62BD98FCAC8B5CBF6F30E0F26797E20383CE9AE6AAAA2D4FFED0467CD6700C6A79ECEB46F55004CD37EA09AA02FA3AFAA9EB7E4554D7D7D24FA7EF20FF59795BF5DBF518AA654BE1CB372ED2B79A20EDFC674BBADE07879F35F5723B24FE5FE7B7BDCEDB1618FCC8F2F968D230BED119BA8D36FCB95C6EFBC9AC44A39FD55E8758F2B869AA69C1D36278D2AD2C8458D312473ABCCCE0D4A1E419BF58976DB12A8B2975FAD947DFEAD1200ACDBA970E9AAE7184E076C6E3DDEEF8BC916C1EA04B860E6114C98C3A8478F5E13D86E780454617A3D56E881701FB72F99472696D9EC262574B24EF9A6936EB33164DF2ECEB11C5255986C611C9B8744CE07B10A59FA4F18049AEE71B9CF13077B311A77E22E70307D9CBFD78005D3AE91B1CACE7230D211673981C561C8BBDC73063E1613899B720DC7B0CD05B49D82873DCE41B9360811619606C703F2722ECAD6DDC38126FA1E31BA08B5B1BB9812E1FC4D8BDF592CD1A37B678F281638DADB7DC20383F779CE02DDE6C1C546C25E703E91459FCB9814CEFC118E2C77040519093EA0518F9BBBE49475BF2B2B5153B131479385F28F4327AC30A5F87B58EBD2D56FC86974199D8CB42E11B5ED698B7F7B208D90D2F4305C75E163B7BC3CBC646C50038FB7503104C790C80B0C24D549720294A788D9FBA203CD609674F1DDBD46BE0CD61CCED0DB83BE2F85A6C2D7B84B80CBD6F04C37BDF7067570F84A3B9C5489D871B19E880FB1BD176BE03ECA1A98CBC61987D97F7262A7D8D413A8F3532C8017736C0B2EFD07A58AAC06D1864DF85F55E5791FB6606E9C46C60A071D7B68F6DCFB97DFF01F7DC590F84A72A3E78E07EA6AF3FEA211F37C037E2E57AC8AA4ADA30DE885F7B13BDBEC6403D6F76485823BE6E5FDC426FF7FDC535F46F7F3607BAB771A01DE7358EA8735FBFDE409DC3FAB33550DF4F95E14694F0902F1B51A6116FF6FD871EF35F6F1289AF4B00CFFD1C1AFF9087DA473CE2A3BEFFE8235EE9070CDE24D4AC236ABF7B7CF7352744F583C777A9C9345FB5EBACFC82B2756563BEF8225BADE0D8B837F593F4F52A9B12DA27DBAF35DD7ABB5CEBC15D4AB72E04C6DD69A01BBB6EB3ED89B2BDB41EDCF916FEF42C7F56D44DFB346BB34986ECE3C96C1169B6D1ED36BD04DE777F9A8C57679AE377F53EA97775CE3BAF396A3DA3012CF265CB63D191F0940EBC476FBE9E66655647B2E32755B95E2CDDDF5D9E1A7E3B4820FB60822F6E0FEF75F1835C57D57C68DEC7B787755CE7B47090D779DEBCCA7FD1BAA8F3CE50A30D6E0FFFDB59F3BCB898C3ADF3A17A1FDF1ED671595657F98CD7266922232488B7E8F7F0F86E8741BA8CE7A5C4B56547F8BB7C7C2B2E17FFF6BD993CE691DF82C7E3AFFDECB038AD9DD5D007F99B02216BC096E157B787493A3A0E31F8E2FF35932BA6E6BD279763FCF79FDCF86B3F3B93EBA98017793EDBA021CCD75F0B36198CF3A25E6C00EFB5B87D0F2FAA36EF681FFDE8F630AC77F4654783075FDC1E9EF137BAE0FCCF6F0F6D9342FC7FA9269420F8BD852516B6DF4258E2AFFDEC080BFEED705BF67E7AEF0468D37278078CF7F1FF6BA6D185F5EF3D954389895B4CE7F0AB3F3B537A7CD1990AFEE0F6EF7F9E2F674866FA20CC67FFAF99CAA194C92D249233CDEF3F8DF1D77E76A6F0C3A5F2797E9997110DEB7F1E8546466B56F00ACC4F66E55A759F24E71D1C3035628145B1CC884C3F1B7C719BD93C7DD7E6F5322B7F34A3EF1D4871E43B6DFB68055FBC37BC97F36A99BF582F265D0512FBFEBDA19F2EB2A23C9ECDEABCE9B848D106EF01FF6BD8B098AC040CF9C39597AFA9472559F4DE92C7CB5DEF2F71F1D7FEDF2A71C3ECF675F96C503CBEA65C809ECBDE38DDA7B787F4326B9AABAAEE50DC7D7A7B48670D7BF11D9CDCA7B787848C6713D178FEE7FFAF11A4D7666DF7BD6549DFFC1AE234F8E6CF8E44FD5EF9355E77AFF307B77FFF9B4E2CB2D20D21E9473F7CAE08B3EB1DD6F096EB23B932F3D56D12625809E850A1BF12DF1FFCAD984160C4580283B7DDBE1746BAC6F135317A6F5C9C413E6B5EACCBF2B38FCEB3B2E9F0C8D038BB4B24EF3DCFB2328559EB4EB3F7CD6D3263119A5A081F38C90CE2BDE9BA099F1FE6147FF014F1CA3FE757BA53E47D739B382142120BE103A78841BC1F5D6EC0E7873945B795C281517E33336C532ED15976DF7EC84C1B281F38DB16CC7B93F926BCFEDF38EB1B46FBC1330FB7F7F797BC5077DAFDAF6231C9505A28426307EA0309CC80DE9BCA1B11FA304E3444784FA43E78DED894A0E3DDB8DDD4AF3EC47032880F248FC0783FDADC84D18771D0FBE1F2CDCDD3DEF03CED7DF83CED7D03F3B4F78DCED3DE0F759E6EAB4A87C6F9CDCCB35D297C723DE0CDFA0D3E64CE3D401F38F33EA4F7A6F92DB0FB6172C137338B6681766812FDEF3F640E1D9C0F9C420FD0FBD1EB56B8FD3027F0B662BC71C4377381C94B7002BC58E675B7894D7CE827F6EFC67C8089CF2EF22FAA595E9A0F9926F37C91312D9A553605AAD4E25951372D18689235B934F92825025C16B43E482278DDB4F9622CB98D5F549E94458E2C9D69F045B62CCEF3A67D53BDCD979F7DB4B7B373F0517A5C165983645679FE51FA6E512EE98F79DBAE1EDDBDDB7007CD78514CEBAAA9CEDBF1B45ADCCD66D55D7AF5E1DD9DBDBBF96C71B76966A53FA95ECA4DA752322C21692979D59D303391AFF2738F07BA53D27DD1BEE6BD83BE3FFBA8C0D859AE68FD34AF692161F6326BB14480563963F9510AD6C826656ED9A3D361077C902C937E9697593D9D67F5D6227B77C707D8D6FD6C5817DEEBE207B926533DACDF13A9E33A27A2E5759E37AFF25FB42EEADC126152BC3FB86F67CDF3E262DEAA27FFF5801C97657595CF38EF4C847ECF31FA09C18DBCD54FD3FD7F96B568ADAB8670E76F0AAC1E484F33FABBE5BFDF131A69C66F0C96CBD945E7AFCFE7B79EBEBE99FBFFECF47942F822CF671F26821E30B22DE745BD507880F735E0BDA8DADC8AF387292CE3927D6915E0D7E62B637D3783BA0D565F53CD44866732B45F1F840B553F0CC6DEFB48DC868952DFE6EB23D371926E89D1AD7500A77DFFFFA103F0EF8D52762B482708045AC2EAEB80BB35E96D2EF6FF1FE43FBEB0E4FA3A7C4E9890977C23C13F80D5FB59CEFFCFD2FA56AC7E1B3DF53CBFCCCB0FD6DB1C814DDB6F0C2B85F7725E2DF317EBC5E46BF1C520D8D3455694C7B3599D37DF8C55BEBDBAB80DB4A705829C45B1CC8869BA103F4ABFC8DE3DCF9717EDFCB38F76F70EDE7FAACCDAD32D0D490C3F6F1DE396506E2DA3C8E8FF48464340DF38BF7ED37285495B7E53A37D9935CD5555DBB9F92060670DFBA616B3AF130DBCAACABC794F15796B7EA7C4564B09A9FF9FB03C75FF8D4CDB379DE7F9C9AC5C5B1E780F48B79EC6E185D4DBCDE5F08268BFED86B5CA9B19C076745B4E8E5B10DBFF6DC10C51D2CBF11A89400C184B5A524E25852C9A94A6F68DDCE9583FF9625DB6C5AA2CA6D4D3671FED8CC7BBBDC138289CBCF281C807218C6FF500486680069395E44F346D4DA9E7B63F6BC5725AACB232C0B8D3EA96520DE25978DD6F9EE62BF8CDCBD61FD46DFA7159A57E6F166887B96E1A7A90B2DF3CCBB2C0D44F1FBEE7F4ECF65669BE5C3EA568B9CD5328FE6A89F8B99966B33E5763F562A86F4E8CF97DCB073F2BAC71EB19FB40CE88AD6945FBB149989F3BC6E024CE2067B092F567473EF8FF1D67F48D8936FBB9E20C975BFBB9658DBD0F638D1BECC2CFFB29DEFB399E622F773A38D11C27FA93241FFC2CEB801F3E83F4E3616DF673C520DDC4F6CF1D9BB8C4F88771C9FF8BD4C1FFEB66BBB3F8F07332D91C6B449629DC14F197FE14C907EF35CDB7B21CDFD034F707A3CDBEE169BEB575B149C19FBB091E580C7113641BF893E43EFC7FED64C707A64D7FAE263CC8E1FE9C4C3A2722806FF3FBBFAED6F5B4A38FDE5789FF2CB9FB415E47707038B82F7E5618E787650C867357DCFC7D92553F64C67993D517F97006E15652FEFF43C6B9F544FEB0196743E6F26785794E39E5C8AB7E05E5B80D16D52C7F56D44DFB346BB349D6F4350FDEA2C4BC9F29FB28958FFB394738C48BECB38F66938A265A12A0FC551331402164C968F500CBC731B8F8E666B0E2A0F6C0CAC731B0F8E666B0E254F5C0CAC731B0F8E666B0CE84F740BBAF62E0CDB7377721B2D6032F1FC740B37AB911ACD8A11E58F9380616DFDC0CD62C094518C37C13E50DF9F266F8A1068AA2EFBE1E1A8669D1EFCE93C590D335A99F7A0D3C868FA5FC038D1A882141B69FF414897BC717307E453EE8EAF110E55B0CC765AF23A319486DBF3F62C11BBE50F31BF2C13733144EEC0D8E45BE1D46CD6724464D3EF8B91CCCDEC6C174F287FF6F1E8C9F0B1BE4B5A17C5980A6AFAD184DF9E0E76A605EF666685C43099EFF970E8B3D1BB186FD01B92F8711F30D2C23261F6C18CAAD98F5EB0EC559DF81E10C45B6D1A0DD43D27DF87330B47EDC1919DE0DC1E9073260CC00DB37DD17DFE4503552DA3CD45838F5FE73F2B33A54C45C80617D7AFBDDE3BBE2AAE807F4675BD5D945FE0579FB65C39F5224B1A6B717B9FCF5346F8A0B07E231C15CE61CF239A0A6CDD9F2BC7A594B3CD3C1C83431E18E12FF8BBCCD6614601CD76D719E4D5BFA7A9A370DBB6F3F99956B6A72BA98E4B3B3E597EB76B56E69C8F962525EFBC44048B4A9FFC7777B383FFE7285BF9A6F620884664143C8BF5C3E5917F0B414EF6759D97414E61008C45A9FE7F4B9CC25856E6D7E716D21BDA896B704A4E47B6A42C437F9625512B0E6CBE5EBEC321FC6ED661A86147BFCB4C82E6A12078F82F289716533EAD9EB823AF0DF70FDD19FC4AEB3C5BBA3FF270000FFFFE83EB99F33920000, '4.3.1')

update Users set rolesasint = 2 where Username = 'shenke2'
update Users set rolesasint = 1 where Username = 'shenke'