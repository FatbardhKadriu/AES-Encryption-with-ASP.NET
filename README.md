# AES Encryption

This project was made for assignment 1 in the course "Information Security" - Development of AES text encryption program in ASP.NET.

![Application View](READMEresources/Index.gif)

## Functionality

In this application is implemented AES (Advanced Encryption Standard) encryption algorithm. The two main functions that have been developed are text encryption and decryption.

Encryption has three possibilities:

- Only plaintext is given to be encrypted - In this case an encryption key and an initialization vector are generated  by default. Key which is used in encryption is generated as SHA-256 hash of given key and iv is generated as MD5 hash of given iv. The given key and IV are shown to the user.

- Plaintext and encryption key are given - Key SHA-256 hash is generated so that for any type of input the key used in the algorithm has a fixed length acceptable for AES. An initialization vector is generated and its MD5 hash is attached to the encryption function. The default IV is shown to the user.

- Plaintext, key and initialization vector are given - Key SHA-256 hash and IV MD5 hash are generated so that for any key and IV input are used their acceptable lengths for AES.

It is also worth noting to mention that after successful encryption you can export used key and IV as text file.

![First encryption case](READMEresources/Encryption_1.gif)

![Second encryption case](READMEresources/Encryption_2.gif)

![Third encryption case](READMEresources/Encryption_3.gif)

Decryption can have three cases:

- Ciphertext, key and initialization vector are given

  - If the key and the initialization vector are the same as those used to encrypt the corresponding text, the decryption is performed successfully.
  - If the key or the initialization vector, or both are not the same as those used to encrypt the corresponding text, decryption cannot be performed.

- Ciphertext and key are given - In the absence of the initialization vector decryption can not be performed.

- Only ciphertext is given - In the absence of the key and the initialization vector decryption can not be performed.

![First decryption case/1](READMEresources/Decryption_1.gif)

![First decryption case/2](READMEresources/Decryption_2.gif)

![Second/third decryption cases](READMEresources/Decryption_3.gif)

## Used technology

- ASP.NET and Web Development

- .NET Desktop Framework

## Members

[Fatbardh Kadriu](https://github.com/FatbardhKadriu)

[Arbena Musa](https://github.com/ArbenaMusa)

[Albana Hysenaj](https://github.com/albanah)
