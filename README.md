# Web API Project using .NET Core
---
## Genshin Impact Inspired Project by Group 9
![image](https://user-images.githubusercontent.com/95534475/195744544-7a485c65-4262-49f1-aeb1-c8998333e0c3.png)
----
## Genshin Impact Entity Relationship Diagram

![ERD GenshinDatabase 2](https://user-images.githubusercontent.com/95534475/196377951-3b1f831a-7c23-4c5c-964a-58aa99dabf5f.png)

This Entity Relationship Diagram is focused on the game Genshin Impact specifically on the relationship of the Character with other attributes that a Genshin Character can and must have.

## Genshin Database:
### Tables:
----
  * Character
![image](https://cdn.discordapp.com/attachments/906295750571479074/1031413493926199306/unknown.png)
A `Character` contains its distinct `Id`. Every Character is also assigned with a `Name`, `Rarity`, and `Gender`. Each Genshin Character must have one `Weapon` that is equipped to aid for battle. To make a character stronger one hast to equip an `Artifact Set`. Each characters are also blessed with a certain `Element` by their archons. Additionally their place of origin or `Region` is also specified in this project.
----
  * Artifact Set
![image](https://user-images.githubusercontent.com/111875528/196085425-8a9ce220-dea1-470a-82c1-f7658ab50b65.png)
An `Artifact Set` must have its own specific `Artifact Set Name`. These artifacts, when equipped together, can provide a buff to a character as said on the `Description`.
----
  * Region
![image](https://user-images.githubusercontent.com/95534475/196317000-04e147a6-ec47-4b8d-9c22-5d26fdf7f6a0.png)
The world of Teyvat is composed of different `Regions`. These regions have its own distinct `Id`, and `Name`. It is also specified the `RegionInspiredFrom` and its `RegionDescription`.
----
  * Weapon
 ![image](https://user-images.githubusercontent.com/111875528/196085470-8a2c9b95-3f53-46f8-8303-b2b5b4c4e825.png)
 A `Weapon` also has its own distinct `Id`, `Name` and its `WeaponType`. Every weapon must also have the amount of `Damage` it can inflict and `Rarity` which indicates how rare the weapon is.
 ----
  * Element
![image](https://whatifgaming.com/wp-content/uploads/2022/02/8F0F4111-F79D-43D1-92B3-AF57F1AD7003.png)
Every `Element` has its own distinct `Id` and a `Name`. These elements are given to the `Characters` as a blessing from the Archons.
----
  * WeaponType
![image](https://user-images.githubusercontent.com/95534475/196346150-cf4b4872-4460-444c-9466-841584a76376.png)
The `WeaponType` distinguishes the different kinds of weapon with a distinct `Id` and the `Name` of that type.
----
  * Team_Character
![image](https://user-images.githubusercontent.com/95534475/196370005-d332be4e-3389-454d-9ead-c624565d2896.png)
`Team_Character` is where we add `Characters` to a specific `Team`. A character can exisit in multiple team while a team can have multiple characters.
----
## Project Brief Description

This project shows relationships between different features in Genshin Impact that revolves around the character. It focuses on the character information and as well as its weapon and artifacts. The APIs are created to showcase the way how a character is made, equip certain equipments, and joining a team created by the user. The following are the important files that are used in order for this to succeed.


### Models
* ArtifactSet
* Char
* Region
* Team_Character
* TeamComposition
* Weapon
* WeaponType
----
### DTOS
* ArtifactSetDtos
* CharDtos
* RegionDtos
* Team_CharacterDtos
* TeamCompositionDtos
* WeaponDtos
* WeaponTypeDtos
----
### REPOSITORIES
* ArtifactSetRepositories
* CharacterRepositories
* RegionRepositories
* Team_CharacterRepositories
* TeamCompositionRepositories
* WeaponRepositories
* WeaponTypeDtos

----
### SERVICES
* ArtifactSetServices
* CharacterServices
* RegionServices
* Team_CharacterServices
* TeamCompositionServices
* WeaponServices
* WeaponTypeServices

----
### CONTROLLERS
* ArtifactSetsController
* CharacterController
* RegionController
* Team_CharactersController
* TeamCompositionController
* WeaponController
* WeaponTypeController
