# AES-Encryption

Detyra e parë në lëndën "Siguria e informacionit" - Zhvillimi i programit për enkriptimin e tekstit me AES në ASP.NET.

![Demo](READMEresources/Index.gif#style=centerme)

## Funksionaliteti

Në këtë aplikacion është implementuar algoritmi i enkriptimit AES (Advanced Encryption Standard). Dy funksionet kryesore që janë zhvilluar janë enkriptimi dhe dekriptimi i tekstit.

Enkriptimi ka tre mundësi:

- Jepet vetëm plaintext për tu enkriptuar - Në këtë rast gjenerohet një çelës për ekriptimi dhe një vektor inicializues i çfarëdoshëm të cilët përdoren në funksionin e enkriptimit. Çelësi dhe IV e përdorur i tregohen përdoruesit.

- Jepet plaintext dhe çelësi për enkriptim - Nëse çelësi ka gjatësi adekuate për algoritmin AES përdoret si i tillë, në të kundërtën gjenerohet hash i tij që për çfarëdo lloj hyrje çelësi i cili përdoret në algoritëm të ketë gjatësi fikse të pranueshme për AES. Gjenerohet një vektor i çfarëdoshëm inicializues dhe i bashkangjitet funksionit të enkriptimit. IV e përdorur i tregohet përdoruesit.

- Jepet plaintext, çelësi dhe vektori inicializues - Nëse çelësi dhe IV kanë gjatësi adekuate për algoritmin AES përdoren siç janë dhënë nga përdoruesi, në të kundërtën gjenerohet hash i çelësit dhe hash i IV-së në mënyrë që për çfarëdo lloj hyrje të çelësit dhe IV të përdoren gjatësitë e pranueshme të tyre për AES.

| ![Demo](READMEresources/Encryption_1.gif#style=centerme) | ![Demo](READMEresources/Encryption_2.gif#style=centerme) |
| -------------------------------------------------------- | -------------------------------------------------------- |

![Demo](READMEresources/Encryption_3.gif#style=centerme)

Dekriptimi mund të ketë tre raste:

- Jepet ciphertext, çelësi dhe vektori inicializues

  - Nëse çelësi dhe vektori inicializues janë të njejtë me ata që janë përdorur për enkriptimin e tekstit përkatës, realizohet dekriptimi me sukses.
  - Nëse çelësi ose vektori inicializues, apo të dy bashkë nuk janë të njejtë me ata që janë përdorur për enkriptimin e tekstit përkatës nuk mund të realizohet dekriptimi.

- Jepet ciphertext dhe çelësi - Në mungesë të vektorit incializues nuk mund të realizohet dekriptimi.

- Jepet vetëm ciphertext - Në mungesë të çelësit dhe vektorit inicializues nuk mund të realizohet dekriptimi.

| ![Demo](READMEresources/Decryption_1.gif#style=centerme) | ![Demo](READMEresources/Decryption_2.gif#style=centerme) |
| -------------------------------------------------------- | -------------------------------------------------------- |

![Demo](READMEresources/Decryption_3.gif#style=centerme)

<style>
  img[src$="centerme"] {
  display:block;
  margin: 0 auto;
  border-radius: 10px;
  }
  h1{
    background-image: url(https://assets.kpmg/content/dam/kpmg/xx/images/2019/10/glass-texture-against-blue-background.jpg/jcr:content/renditions/cq5dam.web.1082.378.jpg);
    background-size: cover;
    border-radius: 5px;
  }
</style>
