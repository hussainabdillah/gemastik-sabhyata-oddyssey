Halo, ada yang bisa saya bantu? #speaker:Profesor
Ya, saya ingin bertanya, apakah di kota Solo terdapat seni budaya? #{speaker=CharacterName}
Ya, di kota Solo terdapat beragam seni budaya yang menarik, salah satunya adalah batik parang. Apakah Anda tertarik?#speaker:Profesor #speaker:???
Benar sekali... #{speaker=CharacterName}?
Ya, memang menarik. Perkenalkan, saya Profesor. Maaf, boleh tahu nama Anda? #speaker:Profesor
-> main


=== main ===
Bagaimana kabar kamu? #speaker:Profesor

+ [Bahagia]
    Owh itu membuatku bahagia juga.
+ [Sedih]
    Owh itu membuatku sedih juga.
    
- Baik, apakah ada pertanyaan lagi? #speaker:Profesor
+ [Iya]
    -> main
+ [Ga]
    Okey
    -> END