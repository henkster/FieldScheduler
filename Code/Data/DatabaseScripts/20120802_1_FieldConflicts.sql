﻿CREATE TABLE [FieldsConflictFields] (
    [Field_Id] [int] NOT NULL,
    [Conflict_Field_Id] [int] NOT NULL,
    CONSTRAINT [PK_FieldsConflictFields] PRIMARY KEY ([Field_Id], [Conflict_Field_Id])
)
CREATE INDEX [IX_Field_Id] ON [FieldsConflictFields]([Field_Id])
CREATE INDEX [IX_Conflict_Field_Id] ON [FieldsConflictFields]([Conflict_Field_Id])
ALTER TABLE [FieldsConflictFields] ADD CONSTRAINT [FK_FieldsConflictFields_Fields_Field_Id] FOREIGN KEY ([Field_Id]) REFERENCES [Fields] ([Id])
ALTER TABLE [FieldsConflictFields] ADD CONSTRAINT [FK_FieldsConflictFields_Fields_Conflict_Field_Id] FOREIGN KEY ([Conflict_Field_Id]) REFERENCES [Fields] ([Id])
INSERT INTO [__MigrationHistory] ([MigrationId], [CreatedOn], [Model], [ProductVersion]) VALUES ('201208020551596_FieldConflicts', '2012-08-02T07:43:33.873Z', 0x1F8B0800000000000400ECBD07601C499625262F6DCA7B7F4AF54AD7E074A10880601324D8904010ECC188CDE692EC1D69472329AB2A81CA6556655D661640CCED9DBCF7DE7BEFBDF7DE7BEFBDF7BA3B9D4E27F7DFFF3F5C6664016CF6CE4ADAC99E2180AAC81F3F7E7C1F3F22FEC7BFF71F7CFC7BBC5B94E9655E3745B5FCECA3DDF1CE4769BE9C56B36279F1D947EBF67CFBE0A3DFE3E8374E1E9FCE16EFD29F34EDF6D08EDE5C369F7D346FDBD5A3BB779BE93C5F64CD78514CEBAAA9CEDBF1B45ADCCD66D5DDBD9D9D83BBBB3B777302F111C14AD3C7AFD6CBB658E4FC07FD79522DA7F9AA5D67E517D52C2F1BFD9CBE79CD50D317D9226F56D934FFECA3A7599B7D941E9745467DBFCECBF3F74464E72110F9C876419D9C1232EDF59BEB55CE1D7DF6D1B3222F677E136AF47BE5D7C107F4D1CBBA5AE5757BFD2A3FD717CF661FA577C3F7EE765FB4AF79EFA06FFA6DD9DEDBFB287DB12ECB6C52D207E759D9E41FA5AB4F1FBD6EAB3AFF3C5FE675D6E6B39759DBE6F512EFE68CBBD2E0D1EAD3DB91E1E1DD9D3D90E16EB65C566DD6D2A4F610EFA0F9346FA675B192A682EFEBB62636F9287D56BCCB67CFF3E5453BB7387F91BD339FD0AF1FA55F2D0BE22A7AA9ADD7B93F46F97B73DFAF8B1FE4C70D91E7264A6D06735CE73455799DE7CDABFC17AD8B3AB7A47F5255659E2DDF1BE4B7B3E67971316F9B0F05745C96D5553E3B9EB6C5254DE8D719ED8BECB2B8E0B9EC92AFAC80E0ABBCE46F9B79B112D119E39BDF5FB83D7D56578B5715C0BB4F7FFF37597D910391AAF7D5EB6A5D4FDF030B7EABA10FE7C5A4688971DECC8B3856027FA0BD437343338B9CC17B535B33C6AF37907CF6E4FA677324DD19B8CDA8FD913CBEEBD4DB46A587A9FD91CE0B05A7CDEA960C4EFE868C95C1D8FDFD9E227EBA9C7D7D589BB9F1EB0977574822727F5B2C3E87818E62816F7E7FE62D0F09FB618FBDDD371FC4CC00F323661EB27E2FF27CF6E1B6CF03480EDC79512F3E1CE68BAACDAD2DFDE1F91744EAD9BACC675F5ADFE6F692D9817592912F7B23A8DB60F5B3E60B7C1D39EDEA8ABE04DF16833779B6D81D4641BFEEE0C09FC69190AFBE0E167B9BB1D88B62B1378CC5DE7B636119EFC9F58649F11B75E7C67D3730455E83F7C5CEB0F226E4FC361DDCDC5771D4BCEF6398DD5AD783F414114E9AB6CEA6ADCAD68F747FA05633E773FCD0B4EAF3FC322FBF51E57552AE275156040BFCFEF2AD6342FB618FFDDC37EF2B124F4923374CF0212C5C8B0E26E68B3836F6DB0F120526C18F58FF6795F56FE705D0E0287A687FD6BBBE356B58BEFC117B044ED6859DA25B6AA76EE4932F6779FD8D4E735FB9DE7A96A100C41E3EC99A5C7132BAA933F5834AEE8B6C995D50AA37AAE4BE6AF29ABD9D2083E13EED4574DE571F14D201CE8F98F71BD46DB7E2BC7EA7A78BAC288F67B33A6F7EF891DACB79B5CC5FAC17939F7591EBF70D065C7ED3D6E4361DBFCC9AE6AAAA2D4FFED03A3E6B3806B523FEBA513D1441F38D7A82AA80BE8E7EEABA5F11D5F5B5F4D3E93BC87F56DE56FD763D866AD952F8F273E4AE48E73F5BD2F53E38FCACA997DB21F1FF3ABFED75DE22BFFF23CB17A049C3F84667E836DAF0E77205F427B3128DBED95E6FC992C74D534D0B9E16C3936E6521C49A9638D2E16506A70E25CFF8C5BA6C8B55594CA9D3CF3EFA568F065168D6BD74D0748D2304B7331EEF76C7E78D64F3005D327408A34866D421C4AB0FEF31BCC872C8665AED867811B02F974F2997D6E6292C76B544F2AE9966B33E63D124CFBE1E515C9265681C918C4BC704BE0751FA491A0F98E47ABEC1190F73371B71EA27723E7090BDDC8F07D0A593BEC1C17A3ED210623187C961C5B1D87B0C33161E8693790BC2BDC700BD95848D32C74DBE310916689101C606F77322C2DEDAC68D23F1163ABE01BAB8B5911BE8F2418CDD5B2FD9AC71638B271F38D6D87ACB0D82F373C709DEE2CDC641C556723E904E91C59F1BC8F4418C21BE03FFDB906B352F2605BCEA37F36258056E78274686A817B2890E9BE0DFD6CBB9AD8A14CF8E43AC82DC762FE4CADFF59D1CB4A5B8C3A71DC562CE3B0C31EA0D307C1DFE4BEC6DF16B6E7819BC127B5978EE8697350BD07B59D4CE0D2FC328C55E16CFE386978DD58E017016FD062010821800118E9BA82E616394F01A51764178AC13CE9EBAFAA9D7C09BC3582010F0792414B0D85AF60871197ADF8885F7BEE1CEAE660C47738B913A9F3F32D0818020A2FFFD90C043531979C330FB41C04D54FA1A83743E7C6490030E7E8065DFC5F7B05481DB30C8BE53EFBDAE22F7CD0CD289D9C040E3CE7E1FDB9EBBFFFE03EE39F81E084F557CF0C0FDDC677FD41DAF3F8E6FC4EFF7905595B461BC114FFF267A7D8D817AFEFD90B046BCFFBEB885FEFFFB8B6BE8F1FF6C0E746FE3403BEE7C1C51E7D07FBD813A17FE676BA0BEE73EA88487BCFB88328DF8F7EF3FF498477F93487C5D02780EF9D0F8877CF63EE211AFFDFD471FF1D37F1606BFC93D8F10E2D6DE7C309EDBF8F3373A16B786F84DB82A26236BFD76FBDDE3BBAF39A3AE1F3CBE4B4DA6F9AA5D67E51794642E1BF3C517D96A053FD0BDA99FA4AF57D994C670B2FD5AF3F5B74BD61FDCA57CFD4260DC9D06F4EE4619B6275A2EC82EF2CEB7083F66F9B3A26EDAA7599B4D3224CD4F668B48B38D518AE9250856FA73669C60D31CBFABB34EBD6B2CD379CD51EB190D60912F5B1E8B8EC4CD69FF3D7AF3F5342BB33AB2BC725295EBC5D2FDDD65B0E1B78315081F4CF0C5EDE1BD2E7E90EBB2AC0FCDFBF8F6B08EEB9C569EF23ACF9B57F92F5A1775DE196AB4C1EDE17F3B6B9E17177378C13E54EFE3DBC33A2ECBEA2A9FF1E2364D648404F116FD1E1EDFED304897F1BC35156DD911FE2E1FDF8ACB251C786F268F0530B7E0F1F86B3F3B2C4E8BAF35F441FEA640841FB065F8D5ED6192C28E430CBEF87FCDE48A657EEFC9E594C8FB4F6EFCB59F9DC9F554C08B3C9F6DD010E6EBAF059B0CC679512F3680F75ADCBE8717559B77B48F7E747B18D699FCB2A3C1832F6E0FCFB8675D70FEE7B787B64921FEBF54134ACEE0BD852596E5B885B0C45FFBD91116FCDBE1B6ECFDF4DE09D06E49C17578C37CFCFFA269745990F79ECAA13CCE2DA673F8D59F9D293DBEE84C057F70FBF73FCF9733E47E7D10E6B3FFD74CA56404DE7B1AED22F0FB4F63FCB59F9D29FC70A97C9E5FE66544C3FA9F47A191D19A1570F1D39FCCCAB5EA3E59CB08628102B1C0A2586644A69F0DBEB8CD6C9EBE6BF37A99953F9AD1F70EA438F29DB67DB4822FDE1BDECB79B5CC5FAC1793AE02897DFFDED04F1759511ECF6675DE745CA46883F780FF356C584C560286FCE1CACBD7D4A3925B7B6FC9E3D5C15B485C47E2E2AFFDBF55E286D9EDEBF2D9A0787C4DB9003D97BD71BA4F6F0FE965D6345755DDA1B8FBF4F690CE1AF6E23B38B94F6F0F09E9CF26A2F1FCCFFF5F2348AFCD52F87BCB92BEF935C469F0CD9F1D89FABDF2EBF075FEE0F6EF7FD3894556BA2124FDE887CF156176BDC31A586E97146E6FEEBDAF6E9310C3B240870A0E84590DE80FFE56CC2030622C81C1DB6EDF0B235DF1F89A18BD372ECE209F352FD665F9D947E759D9747864689CDD2592F79E6759C8C3AC75A7D9FBE63699B1084D2D840F9C6406F1DE74DD84CF0F738A3F788AE098FDFE9C5FE94E91F7CD6DE28408492C840F9C2206F17E74B9019F1FE614DD560A0746F9CDCCB04DB94467D97DFB21336DA07CE06C5B30EF4DE69BF0FA7FE3AC6F18ED07CF3CDCDEDF5FF242DD69F7BFEACE39BE1B4A0B4568EC407D208119D07B537923421FC6898608EF89D407CF1B9B1274BC1BB79BFAD587184E06F181E41118EF479B9B30FA300E7A3F5CBEB979DA1B9EA7BD0F9FA7BD6F609EF6BED179DAFBA1CED36D55E9D038BF9979B62B854FAE07BC59BFC187CCB907E80367DE87F4DE34BF05763F4C2EF86666D12CD00E4DA2FFFD87CCA183F38153E8017A3F7ADD0AB71FE604DE568C378EF883B940625DFEB72134E7C5A440C6E8CDBCE83B491BDB76B9839B49E2E2E679D800F903E7E41BCB596C42F10359BA5A9E97C5D46444DE0FD79B19C024A67805A458E675B789CD7CE927F6EFC67C80B9CD2EF22FAA595E9A0F7974F37C91F1A89A5536E5A1CCF26745DDB4E09749D6E4D2E4A39470BF2C68819874F075D3E68BB124B77E5179521639D2B4A6C117D9B238CF9BF64DF5365F7EF6D1DECECEC147E97159640DB299E5F947E9BB45B9A43FE66DBB7A74F76EC31D34E34531ADABA63A6FC7D36A71379B5577E9D5877777F6EEE6B3C5DDA69995FEF47839577F7AC3197C4CD9CBEE3C983978959F7BB3D995C9EE8BF635EF1DF4FDD94705C6CEA2430BE8794D2B49B397598B3522B4CA19CB8F52E8866C52E6563F743AEC800FB2A5D2CFF232ABA7F3ACDE5A64EFEEF800DBBA9F0EEDC27B5DFC20D76CBA87F57B22755CE744B4BCCEF3E655FE8BD6459D5B229024BD37B86F67CDF3E262DEAA96FA7A408ECBB2BACA67BCF040847ECF31FA19E18DBCD5CFD3FE7F96B568B1B38670E76F0A2C1F494F33FABBE5BFDF131A29B56F0C96539ED1F9EBF3F9ADA7AFEFE7FC7F76FA3C217C91E7B30F13410F184C58512F3E0CDE8BAACDAD387F98C2323EF99756017E6DBE32EED76650B7C1EA6BAA99C8F04C8AFEEB8370B98A0F83B1F73E12B761A2D4B9FDFAC874BCE45B62746B1DC079FFFF7FE800FC7BA394DD0AD2097CFD96B0FA3AE06E4D7A9B8CFFFF07F98F2F2CB9BE0E9F1326E425DF48F00F60F57E9AFBFFB3B4BE15ABDF464F3DCF2FF3F283F5364760D3F61BC34AE1BD9C57CBFCC57A31F95A7C3108F6749115E5F16C56E7CD3763956FAF2E6E03ED69812067512C33629A2EC48FD22FB277CFF3E5453BFFECA3DDBD83F79F2AB3F8784B4312C3CF5BC8BA25945BCB2896747E24A321A06F9C5FBF69B9C2A42DBFA9D1BECC9AE6AAAAEDDC7C10B0B3867D538BD9D7F1DE5F5565DEBCA78ABC35BF53620BC9B6FF9FB03C75FF8D4CDB379DE7F9C9AC5C5B1E780F48B79EC6E195F4DBCDE5F08A78BFED86C5EA9B19C076745B4E8E5B10DBFF6DC1DC9A9243D9F5DBD17143A23942C89BD2D33793F386DCCCEDE8D947E3B6C086A8EA65CE8D9E41641D232B65AA5268389328D6BE91911EEB275FACCBB658118AD4D3671FED8CC7BBBD2139289C12F481C807218C6FF50048BE85069395448FA6AD29A1DFF6A7B0584E8B55560618775ADD52578278165EF79BA7F90AD1C8B2F507759B7E7C4EEAF666817678ECA6A1070B219B6759D66DFB49D9F79C9EDDDEC2D697CBA7948368F314E6B45A222BD14CB3599FB7B19C33D437A71BFDBEE5839F15D6B8F58C7D2067C4968AA3FDD8D456BF330BF3679931383536C8196CBAFCD9910FFE7FC7197D13ADCD7EAE38C3652C7F6E5963EFC358E306BBF0F37E8AF77E8EA7D8CB480F4E3447DFFE24C907FFBFD301FD2C8336FB6619E4F60CD25D2EF8B96313B7DCF0615CF2FF2275F0FFBAD9EE2CE9FC9C4C36477091C51F3745FCA53F45F2C17B4DF3AD2CC73734CDFDC168B36F789A6F6D5D6CAAF5E76E82079698DC04D906FE24B90FFF5F3BD9F18169D39FAB090F32E33F2793CEE91DE0DBFCFEAFAB753DEDE8A3F755E23F4BA63EC8967571705FFCAC30CE0FCB180C6704B9F9FBA4007FC88CF326AB2FF2E10CC2ADA4FCFF878C73EB89FC6133CE867CF00F897924EBC5FF3684DFBCA065165AD478332F6E5443B74C3A6E323B5EC6B807270AEC1B62881F5AF6712825CE8DFF5F9783DCC40C37A8961F31C337CE0C37AE71FCAC70C529AF4AD03B2DBD91D7169759FEACA89BF669D66693ACE96B05BC452BA23E277D94CAC77DA640CCBCC83EFB6836A968AE658D44D82EC21F2164497AF700CBC731B8F8E666B012C32AD86E681B038B6F6E062B71570FAC7C1C038B6F6E06EBBCFC1E68F7550CBCF9F6E62EC41CF7C0CBC731D0EC81DC08565CD51E58F9380616DFDC0CD6ACC54718C37C13E50DF9F266F8A1931245DF7D3D340CD3E2E6EE7C9D1797A11B05C9688F01B1F2A43F942D5D694CBD069E88C5D6216376C00EC87ED2535DEE1D5FA4F915F9A0EB5C8628DF62386E492D329A81F5B6F7472C78C35723FC867CF0CD0C85571B06C722DF0EA3E6B32EA3261FFC5C0E666FE3603A8B1AFF6F1E8C9FA01FE4B5A1247E80A6AF1F194DF9E0E76A605E4A79685C4359E7FF970E8BC32DB1BFFD01B92F8711F34D3A23261F6C18CAAD98F5EB0EC5D9FB81E10CA5DB3C04BBEE0423E93EFC39185A3F191619DE0D19B30F64C098C9B76FBA2FBEC9A16A8CB579A8B140ECFDE7E4E764A89BE2CBE169BEC55BC343FB3AFE48DFFB722F46DFFE864931CC06B778EBFFBDA4407A10106C6C69BF7B7C57FC58FD80FE6CAB3ABBC8BFA0A8B36CF8538A68D7F4F62297BF9EE64D71E1403C2698CB9CB3930EA86973B63CAF4C74DDC1C834315FEBE47C91B7D98C02DDE3BA2DCEB3694B5F4FF3A6E130E227B3724D4D4E17937C76B6FC72DDAED62D0D395F4CCA6B9F1808CD37F5FFF86E0FE7C75FAEF057F34D0C81D02C6808F997CB27EB02D3A4783FCBCAA66346874020E6FF3CA7CF652E2985D0E617D716D28B6A794B404ABEA72655F1265FAC4A02D67CB97C9D5DE6C3B8DD4CC390628F9F16D9454D4AD2A3A07C62029C8C7AF6BAA00EFC375C7F47BF7142EC3A5BBC3BFA7F020000FFFF45D308E409A00000, '4.3.1')