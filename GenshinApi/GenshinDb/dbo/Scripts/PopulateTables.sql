USE [GenshinDb]
GO

INSERT INTO ArtifactSet(Name, Description)
VALUES
('Gladiators'' Finale', '2 Pieces: ATK +18%. ' + 
						'4 Piece: If the wielder of this artifact set uses a Sword, Claymore
						or Polearm,increases their Normal Attack DMG by 35%.'),

('Wanderer''s Troupe', '2 Piece: Increases Elemental Mastery by 80. ' +
						'4 Piece: Increases Charged Attack DMG by 35% if the character uses a Catalyst or Bow.'),

('Noblesse Oblige', '2 Piece: Elemental Burst DMG +20%. ' +
					'4 Piece: Using an Elemental Burst increases all party members'' ' +
					'ATK by 20% for 12s. This effect cannot stack.'),

('Bloodstained Chivalry', '2 Piece: Elemental Burst DMG +20%. ' +
						'4 Piece: Using an Elemental Burst increases all party members'' ' +
						'ATK by 20% for 12s. This effect cannot stack.'),

('Maiden Beloved', '2 Piece: Character Healing Effectiveness +15%. ' +
					'4 Piece: Using an Elemental Skill or Burst increases ' +
					'healing received by all party members by 20% for 10s.'),

('Viridescent Venerer', '2 Piece: Anemo DMG Bonus +15%. ' +
						'4 Piece: Increases Swirl DMG by 60%. Decreases opponent''s Elemental ' +
						'RES to the element infused in the Swirl by 40% for 10s.'),

('Archaic Petra', '2 Piece: Geo DMG Bonus +15%. ' +
					'4 Piece: Upon obtaining an Elemental Shard created through a Crystallize ' +
					'Reaction, all party members gain 35% DMG Bonus for that particular element ' +
					'for 10s. Only one form of Elemental DMG Bonus can be gained in this manner at any one time.'),

('Retracing Bolide', '2 Piece: Increases Shield Strength by 35%. ' +
					'4 Piece: While protected by a shield, gain an additional ' +
					'40% Normal and Charged Attack DMG.'),

('Thundersoother', '2 Piece: Electro RES increased by 40%. ' +
					'4 Piece: Increases DMG against opponents affected by Electro by 35%.'),

('Thundering Fury', '2 Piece: Elemental Burst DMG +20%. ' +
					'4 Piece: Using an Elemental Burst increases all party members'' ' +
					'ATK by 20% for 12s. This effect cannot stack.'),

('Lavawalker', '2 Piece: Pyro RES increased by 40%. ' +
				'4 Piece: Increases DMG against opponents affected by Pyro by 35%.'),

('Crimson Witch of Flames', '2 Piece: Pyro DMG Bonus +15%. ' +
							'4 Piece: Increases Overloaded and Burning, and Burgeon ' +
							'DMG by 40%. Increases Vaporize and Melt DMG by 15%. Using ' +
							'Elemental Skill increases the 2-Piece Set Bonus by 50% of its starting value for 10s. Max 3 stacks.'),

('Blizzard Strayer', '2 Piece: Cryo DMG Bonus +15%. ' +
					'4 Piece: When a character attacks an opponent affected by Cryo, their CRIT Rate ' +
					'is increased by 20%. If the opponent is Frozen, CRIT Rate is increased by an additional 20%.'),

('Heart of Depth', '2 Piece: Hydro DMG Bonus +15%. ' +
					'4 Piece: After using an Elemental Skill, increases Normal Attack and Charged Attack DMG by 30% for 15s.'),

('Tenacity of the Millelith', '2 Piece: HP +20%. ' +
					'4 Piece: When an Elemental Skill hits an opponent, the ATK of ' +
					'all nearby party members is increased by 20% and their Shield Strength ' +
					'is increased by 30% for 3s. This effect can be triggered once every 0.5s. ' +
					'This effect can still be triggered even when the character who is using this artifact set is not on the field.'),

('Pale Flame', '2 Piece: Physical DMG Bonus +25%. ' +
				'4 Piece: When an Elemental Skill hits an opponent, ATK ' +
				'is increased by 9% for 7s. This effect stacks up to 2 times and ' +
				'can be triggered once every 0.3s. Once 2 stacks are reached, the 2-set effect is increased by 100%.'),

('Shimenawa''s Reminiscence', '2 Piece: ATK +18%. ' +
							'4 Piece: When casting an Elemental Skill, if the character has 15 or more Energy, they lose 15 ' +
							'Energy and Normal/Charged/Plunging Attack DMG is increased by 50% for 10s. ' +
							'This effect will not trigger again during that duration.'),

('Emblem of Severed Fate', '2 Piece: Energy Recharge +20%. ' +
							'4 Piece: Increases Elemental Burst DMG by 25% of Energy Recharge. ' +
							'A maximum of 75% bonus DMG can be obtained in this way.'),

('Husk of Opulent Dreams', '2 Piece: DEF +30%. ' +
							'4 Piece: A character equipped with this Artifact set will. ' +
							'obtain the Curiosity effect in the following conditions: ' +
							'When on the field, the character gains 1 stack after hitting an opponent with ' +
							'a Geo attack, triggering a maximum of once every 0.3s. ' +
							'When off the field, the character gains 1 stack every 3s. ' +
							'Curiosity can stack up to 4 times, each providing 6% DEF and a 6% Geo DMG Bonus. ' +
							'When 6 seconds pass without gaining a Curiosity stack, 1 stack is lost.'),

('Ocean-Hued Clam', '2 Piece: Healing Bonus +15%. ' +
					'4 Piece: When the character equipping this artifact set heals a character in ' +
					'the party, a Sea-Dyed Foam will appear for 3 seconds, accumulating the amount of ' +
					'HP recovered from healing (including overflow healing). ' +
					'At the end of the duration, the Sea-Dyed Foam will explode, ' +
					'dealing DMG to nearby opponents based on 90% of the accumulated healing. ' +
					'(This DMG is calculated similarly to Reactions such as Electro-Charged, and Superconduct, but it is not  ' +
					'affected by Elemental Mastery, Character Levels, or Reaction DMG Bonuses). ' +
					'Only one Sea-Dyed Foam can be produced every 3.5 seconds. ' +
					'Each Sea-Dyed Foam can accumulate up to 30,000 HP (including overflow healing). ' +
					'There can be no more than one Sea-Dyed Foam active at any given time. ' +
					'This effect can still be triggered even when the character who is using this artifact set is not on the field.'),

('Vermillion Hereafter', '2 Piece: ATK +18%. ' +
						'4 Piece: After using an Elemental Burst, this character will gain the Nascent Light effect, ' +
						'increasing their ATK by 8% for 16s. When the character''s HP decreases, their ATK will further ' +
						'increase by 10%. This increase can occur this way maximum of 4 times. This effect can be triggered ' +
						'once every 0.8s. Nascent Light will be dispelled when the character leaves the field. If an Elemental ' +
						'Burst is used again during the duration of Nascent Light, the original Nascent Light will be dispelled.'),

('Echoes of an Offering', '2 Piece: ATK +18%. ' +
							'4 Piece: When Normal Attacks hit opponents, there is a 36% chance that it will ' +
							'trigger Valley Rite, which will increase Normal Attack DMG by 70% of ATK. ' +
							'This effect will be dispelled 0.05s after a Normal Attack deals DMG.\r\n ' +
							'If a Normal Attack fails to trigger Valley Rite, the odds of it triggering the next time will increase by 20%. ' +
							'This trigger can occur once every 0.2s.'),

('Deepwood Memories', '2 Piece: Dendro DMG Bonus +15%. ' +
						'4 Piece: After Elemental Skills or Bursts hit opponents, the targets'' Dendro RES ' +
						'will be decreased by 30% for 8s. This effect can be triggered even if the equipping character is not on the field.'),

('Gilded Dreams', '2 Piece: Elemental Mastery +80. ' +
					'4 Piece: Within 8s of triggering an Elemental Reaction, the character ' + 
					'equipping this will obtain buffs based on the Elemental Type of the other ' +
					'party members, ATK is increased by 14% for each party member whose Elemental Type is ' +
					'the same as the equipping character, and Elemental Mastery is increased by 50 ' +
					'for every party member with a different Elemental Type. Each of the aforementioned buffs will count up ' +
					'to 3 characters. This effect can be triggered once every 8s. The character who equips this can ' +
					'still trigger its effects when not on the field.');

INSERT INTO Weapons ([Name],Damage,WeaponType_Id,Rarity)
VALUES
('Summit Shaper',46,1,5),
('Skyward Blade',46,1,5),
('Primordial Jade Cutter',44,1,5),
('Mistsplitter Reforged',48,1,5),
('Haran Geppaku Futsu',46,1,5),
('Freedom-Sworn',46,1,5),
('Aquila Favonia',48,1,5),
('Vortex Vanquisher',46,2,5),
('Staff of the Scarlet Sands',44,2,5),
('Staff of Homa',46,2,5),
('Skyward Spine',48,2,5),
('Primordial Jade Winged-Spear',48,2,5),
('Engulfing Lightning',46,2,5),
('Calamity Queller',46,2,5),
('Wolf''s Gravestone',46,3,5),
('The Unforged',46,3,5),
('Song of Broken Pines',49,3,5),
('Skyward Pride',48,3,5),
('Redhorn Stonethresher',44,3,5),
('Skyward Atlas',48,4,5),
('Lost Prayer to the Sacred Winds',44,4,5),
('Memory of Dust',46,4,5),
('Kagura''s Verity',46,4,5),
('Everlasting Moonglow',46,4,5),
('Thundering Pulse',46,5,5),
('Skyward Harp',48,5,5),
('Polar Star',46,5,5),
('Hunter''s Path',44,5,5),
('Elegy for the End',46,5,5),
('Aqua Simulacra',44,5,5),
('Amos'' Bow',46,5,5),
('The Flute',42,1,4),
('The Black Sword',42,1,4),
('The Alley Flash',45,1,4),
('Sword of Descension',39,1,4),
('Sapwood Blade',44,1,4),
('Sacrificial Sword',41,1,4),
('Royal Longsword',42,1,4),
('Prototype Rancour',44,1,4),
('Lion''s Roar',42,1,4),
('Kagotsurube Isshin',42,1,4),
('Iron Sting',42,1,4),
('Festering Desire',42,1,4),
('Favonius Sword',41,1,4),
('Cinnabar Spindle',41,1,4),
('Blackcliff Longsword',44,1,4),
('Amenoma Kageuchi',41,1,4),
('The Catch',42,2,4),
('Royal Spear',44,2,4),
('Protoype Grudge',42,2,4),
('Moonpiercer',44,2,4),
('Lithic Spear',44,2,4),
('Kitain Cross Spear',44,2,4),
('Favonius Lance',44,2,4),
('Dragon''s Bane',41,2,4),
('Dragonspine Spear',41,2,4),
('Deathmatch',41,2,4),
('Crescent Pike',44,2,4),
('Blackcliff Pole',42,2,4),
('Whiteblind',42,3,4),
('The Bell',42,3,4),
('Snow-Tombed Starsilver',44,3,4),
('Serpent Spine',42,3,4),
('Sacrificial Greatsword',44,3,4),
('Royal Greatsword',43,3,4),
('Rainslasher',42,3,4),
('Prototype Aminus',44,3,4),
('Makhaira Aquamarine',42,3,4),
('Luxurious Sea-Lord',41,3,4),
('Lithic Blade',42,3,4),
('Katsuragikiri Nagamasa',42,3,4),
('Forest Regalia',44,3,4),
('Favonius Greatsword',41,3,4),
('Blackcliff Slasher',42,3,4),
('Akuomaru',42,3,4),
('Wine and Song',42,4,4),
('The Widsith',42,4,4),
('Solar Pearl',42,4,4),
('Sacrificial Fragments',41,4,4),
('Royal Grimoire',44,4,4),
('Prototype Amber',42,4,4),
('Oathsworn Eye',44,4,4),
('Mappa Mare',44,4,4),
('Hakushin Ring',44,4,4),
('Fruit of Fulfillment',42,4,4),
('Frostbearer',42,4,4),
('Favonius Codex',42,4,4),
('Eye of Perception',41,4,4),
('Dodoco Tales',41,4,4),
('Blackcliff Amulet',42,4,4),
('Windblume Ode',42,5,4),
('The Viridescent Hunt',42,5,4),
('The Stringless',42,5,4),
('Sacrificial Bow',44,5,4),
('Rust',42,5,4),
('Royal Bow',42,5,4),
('Prototype Crescent',42,5,4),
('Predator',42,5,4),
('Mitternachts Waltz',42,5,4),
('King''s Squire',41,5,4);

INSERT INTO Element(Name, Description) 
VALUES
('Pyro', 'Hot'),
('Cryo', 'Cold'),
('Dendro', 'Plant'),
('Hydro', 'Wet'),
('Geo', 'Rock'),
('Anemo', 'Airy'),
('Electro', 'Bzzt');

INSERT INTO Region(Name, RegionInspiredFrom, RegionDescription) VALUES
('Mondstadt','European Countries', 'A Region all about Freedom. This region is led and protected by the Knights of Favonious and their Archon Barbatos'),
('Liyue', 'China', 'A Region all about business, this region is led and protected by the Liyue Qixing, the Adepti, and their Archon Morax'),
('Inazuma', 'Japan', 'A Region that is all about eternity. This region is led governed by the Raiden SHogon, Archon Beelzebub'),
('Sumeru', 'South Asia', 'A Region that is known as the center of learning and houses the Sumeru Akademiya. This region is currently protected by Lesser Lord Kusanali');

INSERT INTO WeaponType(Type) VALUES
('Sword'),
('Polearm'),
('Claymore'),
('Catalyst'),
('Bow')

INSERT INTO Character(Name, Rarity, Gender, WeaponId, RegionId, PreferredArtifactSet, ElemId) 
VALUES
('Albedo', '5*', 'Male', 33, 1, 19, 5),
('Amber', '4*', 'Female', 94, 1, 11, 1),
('Rex Lapis', '5*', 'Male', 8, 2, 8, 1),
('Ningguang', '4*', 'Female', 22, 2, 9, 1),
('Barbatos', '5*', 'Male', 26, 1, 7, 2),
('Xingqiu', '4*', 'Male', 100, 2, 4, 1),
('Jean', '5*', 'Female', 7, 1, 6, 2);