using BloodSaved.Parsing.Attributes;

namespace BloodSaved.Parsing.Enums
{
  public enum ItemId
  {
    [ItemName("Ether"), ItemDescription("Medicine for restoring a small amount of magic."), ItemCategory(ItemCategory.Potion)]
    Ether,

    [ItemName("Ex Ether"), ItemDescription("Medicine for fully restoring magic."), ItemCategory(ItemCategory.Potion)]
    ExEther,

    [ItemName("Ex Potion"), ItemDescription("Medicine for fully restoring health."), ItemCategory(ItemCategory.Potion)]
    ExPotion,

    [ItemName("Faerie Allheal"), ItemDescription("Medicine for fully restoring health. Only used by Carabosse."), ItemCategory(ItemCategory.Potion)]
    FaerieAllheal,

    [ItemName("Faerie Elixir"), ItemDescription("Medicine for restoring a large amount of health. Only used by Carabosse."), ItemCategory(ItemCategory.Potion)]
    FaerieElixir,

    [ItemName("Faerie Medicine"), ItemDescription("Medicine for restoring a small amount of health.  Only used by Carabosse."), ItemCategory(ItemCategory.Potion)]
    FaerieMedicine,

    [ItemName("Faerie Panacea"), ItemDescription("A medicine that dispels all afflictions.  Only used by Carabosse."), ItemCategory(ItemCategory.Potion)]
    FaeriePanacea,

    [ItemName("High Ether"), ItemDescription("Medicine for restoring a large amount of magic."), ItemCategory(ItemCategory.Potion)]
    HighEther,

    [ItemName("High Potion"), ItemDescription("Medicine for restoring a large amount of health."), ItemCategory(ItemCategory.Potion)]
    HighPotion,

    [ItemName("Holy Water"), ItemDescription("Water that has been blessed. Removes curses."), ItemCategory(ItemCategory.Potion)]
    HolyWater,

    [ItemName("Mithridate"), ItemDescription("Medicine for purging poison from the body."), ItemCategory(ItemCategory.Potion)]
    Mithridate,

    [ItemName("Panacea"), ItemDescription("A medicine that dispels all afflictions."), ItemCategory(ItemCategory.Potion)]
    Panacea,

    [ItemName("Poison"), ItemDescription("A substance that poisons you and drains health  over a period of time."), ItemCategory(ItemCategory.Potion)]
    Poison,

    [ItemName("Potion"), ItemDescription("Medicine for restoring a small amount of health."), ItemCategory(ItemCategory.Potion)]
    Potion,

    [ItemName("Stonethaw"), ItemDescription("Medicine that restores flexibility to petrified flesh."), ItemCategory(ItemCategory.Potion)]
    Stonethaw,

    [ItemName("Waystone"), ItemDescription("A relic that takes you wherever you picture. Requires concentration."), ItemCategory(ItemCategory.Potion)]
    Waystone,

    [ItemId("16BitCoin"), ItemName("16-bit Coin"), ItemDescription("A rare and valuable coin that can be found in circulation."), ItemCategory(ItemCategory.Materials)]
    SixteenBitCoin,

    [ItemId("32BitCoin"), ItemName("32-bit Coin"), ItemDescription("An extremely rare and valuable coin that can be found in circulation."), ItemCategory(ItemCategory.Materials)]
    ThirtyTwoBitCoin,

    [ItemId("8BitCoin"), ItemName("8-bit Coin"), ItemDescription("A valuable coin that can be found in circulation."), ItemCategory(ItemCategory.Materials)]
    EightBitCoin,

    [ItemName("Dragon's Wrath"), ItemDescription("A rare scale that is oft said to be the source of a dragon's rage."), ItemCategory(ItemCategory.Materials)]
    Abyssguardianbastard,

    [ItemName("Dragon Heart"), ItemDescription("A heart that continues to pulse angrily."), ItemCategory(ItemCategory.Materials)]
    AbyssguardianHeart,

    [ItemName("Alexandrite"), ItemDescription("A type of chrysoberyl with color-changing properties."), ItemCategory(ItemCategory.Materials)]
    Alexandrite,

    [ItemName("Alkahest"), ItemDescription("A single-use solvent that is used to break down transmuted materials."), ItemCategory(ItemCategory.Materials)]
    Alkhahest,

    [ItemName("Demon Claw"), ItemDescription("A claw from a demon."), ItemCategory(ItemCategory.Materials)]
    Apenail,

    [ItemName("Durable Rag"), ItemDescription("A scrap of cloth once worn by an assassin."), ItemCategory(ItemCategory.Materials)]
    Assasincloth,

    [ItemName("Bat Fang"), ItemDescription("A fang from a bat."), ItemCategory(ItemCategory.Materials)]
    Batfang,

    [ItemName("Small Webbing"), ItemDescription("The wing membrane of a bat."), ItemCategory(ItemCategory.Materials)]
    BatFeather,

    [ItemName("Sinister Heart"), ItemDescription("The heart of an unusually cruel demon."), ItemCategory(ItemCategory.Materials)]
    BelialHeart,

    [ItemName("Bixbite"), ItemDescription("An extremely rare red gemstone."), ItemCategory(ItemCategory.Materials)]
    Bixbite,

    [ItemName("Imbrued Bone"), ItemDescription("A bone steeped in blood."), ItemCategory(ItemCategory.Materials)]
    Bloodybone,

    [ItemName("Imbrued Skull"), ItemDescription("A frightful skull that is bathed in blood."), ItemCategory(ItemCategory.Materials)]
    Bloodyskull,

    [ItemName("Bronze"), ItemDescription("A metal that changes color from added tin."), ItemCategory(ItemCategory.Materials)]
    Bronze,

    [ItemName("Leonine Pelt"), ItemDescription("The thick pelt of a lion demon."), ItemCategory(ItemCategory.Materials)]
    Buerfur,

    [ItemName("Cannon Scrap"), ItemDescription("A scrap from a shattered cannon."), ItemCategory(ItemCategory.Materials)]
    CannonDebris,

    [ItemName("Monster Horn"), ItemDescription("A horn taken from something horrible."), ItemCategory(ItemCategory.Materials)]
    Chicomecoatlhorn,

    [ItemName("Crystal"), ItemDescription("A precious mineral with purifying properties that has been revered as holy since ancient times."), ItemCategory(ItemCategory.Materials)]
    Clystal,

    [ItemName("Confession"), ItemDescription("One might hide this prayer of truths unsaid Or one might slip to Johannes instead."), ItemCategory(ItemCategory.Materials)]
    COL_Confession,

    [ItemName("Diamond (Aurora)"), ItemDescription("A beautiful stone, forever to keep Perfect for Pisceans in the waters so deep."), ItemCategory(ItemCategory.Materials)]
    COL_Diamond,

    [ItemName("Emerald (Aurora)"), ItemDescription("Deep green gem to enhance an ability An entrancing bauble for birds or Capilli."), ItemCategory(ItemCategory.Materials)]
    COL_Emerald,

    [ItemName("Firefly Elixir"), ItemDescription("An odd concoction, aroma most ridiculous Whatever the taste, favorite of Igniculus."), ItemCategory(ItemCategory.Materials)]
    COL_FireflyElixir,

    [ItemName("Gold (Aurora)"), ItemDescription("This rarest of metals is a sight to behold A surprise to no one, our Robert loves gold!"), ItemCategory(ItemCategory.Materials)]
    COL_Gold,

    [ItemName("Orichalcum (Aurora)"), ItemDescription("No simple feat, tempering a shard Requires great skill, and minerals most hard."), ItemCategory(ItemCategory.Materials)]
    COL_Orichalcum,

    [ItemName("Ruby (Aurora)"), ItemDescription("Red jewels, gifts for ladies most winsome Rubella's strength grows from this stone of... scarlet."), ItemCategory(ItemCategory.Materials)]
    COL_Ruby,

    [ItemName("Sapphire (Aurora)"), ItemDescription("A brilliant blue gem, for ages was rumored Among other things, to benefit humors."), ItemCategory(ItemCategory.Materials)]
    COL_Sapphire,

    [ItemName("Hemp"), ItemDescription("Cloth made from plant fibers. It is comfortable to wear and breathes well, but falls apart easily."), ItemCategory(ItemCategory.Materials)]
    Cotton,

    [ItemName("Witch's Tears"), ItemDescription("A liquid as red as blood."), ItemCategory(ItemCategory.Materials)]
    Cyhyraethtear,

    [ItemName("Cypress"), ItemDescription("A fine lumber favored in the East."), ItemCategory(ItemCategory.Materials)]
    CypressLumber,

    [ItemName("Damascus"), ItemDescription("A sturdy metal with attractive banding."), ItemCategory(ItemCategory.Materials)]
    DamascusSteel,

    [ItemName("Sword Fragment"), ItemDescription("One piece of a shattered demon blade."), ItemCategory(ItemCategory.Materials)]
    deathBringerDebris,

    [ItemName("Durable Leather"), ItemDescription("Tough leather that will not tear easily."), ItemCategory(ItemCategory.Materials)]
    Decarabialeather,

    [ItemName("Slimy Leather"), ItemDescription("Tough leather that still glistens with...ugh..."), ItemCategory(ItemCategory.Materials)]
    Decimaleather,

    [ItemName("Dreadful Rag"), ItemDescription("A scrap of cloth once worn by a demon."), ItemCategory(ItemCategory.Materials)]
    DemonCloth,

    [ItemName("Demon Dog Fang"), ItemDescription("A sharp fang from a demon canine."), ItemCategory(ItemCategory.Materials)]
    DemonDogFang,

    [ItemName("Houndskin"), ItemDescription("The hide of a demonic dog."), ItemCategory(ItemCategory.Materials)]
    DemonDogSkin,

    [ItemName("Inhuman Carapace"), ItemDescription("The formidable shell of a demon."), ItemCategory(ItemCategory.Materials)]
    Dethtrapshell,

    [ItemName("Strange Leather"), ItemDescription("Leather with unusual patterning."), ItemCategory(ItemCategory.Materials)]
    DevilBookLeather,

    [ItemName("Tome Scrap"), ItemDescription("A torn page that crackles with magic."), ItemCategory(ItemCategory.Materials)]
    DevilBookPaperStrip,

    [ItemName("Fiend Eye"), ItemDescription("An eye that sees only darkness."), ItemCategory(ItemCategory.Materials)]
    DevilRoyalEyeball,

    [ItemName("Demon Tail"), ItemDescription("A tail from a demon."), ItemCategory(ItemCategory.Materials)]
    DevilRoyalTail,

    [ItemName("Sinister Fang"), ItemDescription("The fang of an unusually cruel demon."), ItemCategory(ItemCategory.Materials)]
    Devilsfang,

    [ItemName("Diamond"), ItemDescription("A gemstone with a beautiful sparkle."), ItemCategory(ItemCategory.Materials)]
    Diamond,

    [ItemName("Faerie Wing"), ItemDescription("A wing plucked from a faerie."), ItemCategory(ItemCategory.Materials)]
    Dineseawing,

    [ItemName("Dragon Bone"), ItemDescription("A precious find, even in this castle."), ItemCategory(ItemCategory.Materials)]
    Dragonbone,

    [ItemName("Dragon Talons"), ItemDescription("A rare find: the talons of a dragon."), ItemCategory(ItemCategory.Materials)]
    Dragonclaw,

    [ItemName("Dragon Scale"), ItemDescription("A scale as rigid as armor."), ItemCategory(ItemCategory.Materials)]
    Dragonscales,

    [ItemName("Ectoplasm"), ItemDescription("A flossy, astral matter."), ItemCategory(ItemCategory.Materials)]
    Ectoplasm,

    [ItemName("Demon Heart"), ItemDescription("The blackened heart of a demon."), ItemCategory(ItemCategory.Materials)]
    EligosHeart,

    [ItemName("Elm"), ItemDescription("A cheap lumber."), ItemCategory(ItemCategory.Materials)]
    ElmLumber,

    [ItemName("Emerald"), ItemDescription("A bright green gemstone."), ItemCategory(ItemCategory.Materials)]
    Emerald,

    [ItemName("Sinister Eye"), ItemDescription("An evil eye, literally."), ItemCategory(ItemCategory.Materials)]
    Evileye,

    [ItemName("Faerie Dust"), ItemDescription("Glittering dust produced from the scales of faerie wings."), ItemCategory(ItemCategory.Materials)]
    FairyPowder,

    [ItemName("Wool"), ItemDescription("Versatile cloth made from sheep's wool that is cool in the summer and warm in winter."), ItemCategory(ItemCategory.Materials)]
    Felt,

    [ItemName("Demon Horn"), ItemDescription("A horn taken from something unspeakable."), ItemCategory(ItemCategory.Materials)]
    Gaaphorn,

    [ItemName("Demon Wing"), ItemDescription("A wing from a demon."), ItemCategory(ItemCategory.Materials)]
    Gaapwing,

    [ItemName("Hellhorse Mane"), ItemDescription("The mane of an untameable equine demon."), ItemCategory(ItemCategory.Materials)]
    Gamiginhair,

    [ItemName("Hellhorse Hoof"), ItemDescription("A hoof that has treaded on who knows what carnage."), ItemCategory(ItemCategory.Materials)]
    Gamiginhoof,

    [ItemName("Gargoyle Stone"), ItemDescription("A fragment claimed from the wreckage of a gargoyle."), ItemCategory(ItemCategory.Materials)]
    Gargoyledebris,

    [ItemName("Rat Tail"), ItemDescription("The tail of a giant rat."), ItemCategory(ItemCategory.Materials)]
    Giantrattail,

    [ItemName("Rat Teeth"), ItemDescription("The incisors of a giant rat."), ItemCategory(ItemCategory.Materials)]
    Giantratteeth,

    [ItemName("Imbrued Fang"), ItemDescription("A fang caked with horrific amounts of blood."), ItemCategory(ItemCategory.Materials)]
    Gieremundfang,

    [ItemName("Water Horse Mane"), ItemDescription("The mane of a water horse."), ItemCategory(ItemCategory.Materials)]
    Glashtynhair,

    [ItemName("Water Horse Hoof"), ItemDescription("The hoof of a demon that lurks beneath the water."), ItemCategory(ItemCategory.Materials)]
    Glashtynhoof,

    [ItemName("Gold"), ItemDescription("Not just any gold. This was created by Alchemists."), ItemCategory(ItemCategory.Materials)]
    Gold,

    [ItemName("Gunpowder"), ItemDescription("A powder that burns quickly and explodes, making it useful, but dangerous to handle."), ItemCategory(ItemCategory.Materials)]
    Gunpowder,

    [ItemName("Demon Fang"), ItemDescription("A fang from a demon."), ItemCategory(ItemCategory.Materials)]
    Gusionfang,

    [ItemName("Fiend Pelt"), ItemDescription("A pelt clouded in darkness."), ItemCategory(ItemCategory.Materials)]
    Gusionfur,

    [ItemName("Bovine Plume"), ItemDescription("A feather imbued with the power of levitation."), ItemCategory(ItemCategory.Materials)]
    Haagentifeather,

    [ItemName("Halite"), ItemDescription("Salt extracted from rocks."), ItemCategory(ItemCategory.Materials)]
    Halite,

    [ItemName("Cotton"), ItemDescription("Cloth made from a plant. It is durable, absorbent, and soft on the skin."), ItemCategory(ItemCategory.Materials)]
    Hemp,

    [ItemName("Crimsonite"), ItemDescription("A legendary metal as bright as the sun."), ItemCategory(ItemCategory.Materials)]
    Hihiirokane,

    [ItemName("Iron"), ItemDescription("A common form of metal."), ItemCategory(ItemCategory.Materials)]
    Iron,

    [ItemName("Monster Fang"), ItemDescription("A fang from a monster."), ItemCategory(ItemCategory.Materials)]
    Ivory,

    [ItemName("Sinister Skull"), ItemDescription("A skull that is, against all odds, quite sinister."), ItemCategory(ItemCategory.Materials)]
    Jeneralskull,

    [ItemName("Demon Bone"), ItemDescription("A bone from a demon."), ItemCategory(ItemCategory.Materials)]
    Knightbone,

    [ItemName("Queen's Tail"), ItemDescription("An elegant and regal tail."), ItemCategory(ItemCategory.Materials)]
    Lamastutail,

    [ItemName("Queen's Tears"), ItemDescription("A liquid imbued with powerful sorcery."), ItemCategory(ItemCategory.Materials)]
    Lamastutear,

    [ItemName("Cashmere"), ItemDescription("A fine, downy wool taken from a goat. It is lightweight and quite lovely to look at."), ItemCategory(ItemCategory.Materials)]
    Leather,

    [ItemName("Sinister Rag"), ItemDescription("A scrap of cloth once worn by an especially awful demon."), ItemCategory(ItemCategory.Materials)]
    Lerajecloth,

    [ItemName("Lili Ears"), ItemDescription("The ears of a half-rabbit, half-human."), ItemCategory(ItemCategory.Materials)]
    Lilimear,

    [ItemName("Lili Tail"), ItemDescription("A charming prize taken from a murderous rabbit-lady-thing."), ItemCategory(ItemCategory.Materials)]
    Lilimtail,

    [ItemName("Mahogany"), ItemDescription("A rare and beautiful lumber."), ItemCategory(ItemCategory.Materials)]
    MahoganyLumber,

    [ItemName("Lion Mane"), ItemDescription("A very soft and fluffy mane."), ItemCategory(ItemCategory.Materials)]
    ManeLion,

    [ItemName("Fiend Fang"), ItemDescription("A fang embued with dark energy."), ItemCategory(ItemCategory.Materials)]
    Marbasfang,

    [ItemName("Lion Lord's Mane"), ItemDescription("A mane that continues to exude valor. "), ItemCategory(ItemCategory.Materials)]
    Marbasmane,

    [ItemName("8-bit Nightmare"), ItemDescription("A book inside which an ancient demon overlord has been imprisoned."), ItemCategory(ItemCategory.Materials)]
    Medal022,

    [ItemName("Melting Bone"), ItemDescription("A bone that is slightly sticky to touch."), ItemCategory(ItemCategory.Materials)]
    MeltedBone,

    [ItemName("Melting Skull"), ItemDescription("A skull that looks eerier from melting."), ItemCategory(ItemCategory.Materials)]
    MeltingSkull,

    [ItemName("Mercury"), ItemDescription("A common alchemic material."), ItemCategory(ItemCategory.Materials)]
    Mercury,

    [ItemName("Chair Remnants"), ItemDescription("A fragment claimed from the wreckage of a chair."), ItemCategory(ItemCategory.Materials)]
    Mimicchairdebris,

    [ItemName("Grotesque Shell"), ItemDescription("The hardy shell of a monster."), ItemCategory(ItemCategory.Materials)]
    Mimicshell,

    [ItemName("Aquatic Blood"), ItemDescription("The blood of an aquatic demon."), ItemCategory(ItemCategory.Materials)]
    Misteriousfluid,

    [ItemName("Mithril"), ItemDescription("An argent mineral that is both durable and light. However, only the finest of smiths can work it."), ItemCategory(ItemCategory.Materials)]
    Mithril,

    [ItemName("Monster Fur"), ItemDescription("A fur from a monster."), ItemCategory(ItemCategory.Materials)]
    MonkeyFur,

    [ItemName("Monster Bird Hair"), ItemDescription("The lovely hair of a demon...bird?"), ItemCategory(ItemCategory.Materials)]
    MonsterBirdHair,

    [ItemName("Monster Bird Tear"), ItemDescription("A mysterious glowing liquid."), ItemCategory(ItemCategory.Materials)]
    MonsterBirdTears,

    [ItemName("Fiend Heart"), ItemDescription("A heart that brims with darkness."), ItemCategory(ItemCategory.Materials)]
    MurmurHeart,

    [ItemName("Eastern Fabric"), ItemDescription("A scrap of cloth once worn by a shinobi."), ItemCategory(ItemCategory.Materials)]
    Ninjacloth,

    [ItemName("Oak"), ItemDescription("A fairly sturdy lumber."), ItemCategory(ItemCategory.Materials)]
    OakLumber,

    [ItemName("Obsidian"), ItemDescription("A black gemstone formed by volcanic lava."), ItemCategory(ItemCategory.Materials)]
    Obsidian,

    [ItemName("Orichalcum"), ItemDescription("A legendary metal excavated from a lost country."), ItemCategory(ItemCategory.Materials)]
    Orichalcum,

    [ItemName("Platinum"), ItemDescription("A bright metal resistant to oxidation and corrosion. It is difficult to mine and quite precious."), ItemCategory(ItemCategory.Materials)]
    Platinum,

    [ItemName("Toad Eye"), ItemDescription("A slightly creepy, goggling eyeball."), ItemCategory(ItemCategory.Materials)]
    PoisonToadEye,

    [ItemName("Writhing Limb"), ItemDescription("A branch (?) that continues its disgusting flailing."), ItemCategory(ItemCategory.Materials)]
    Recklesslimb,

    [ItemName("Ruby"), ItemDescription("A scarlet gemstone."), ItemCategory(ItemCategory.Materials)]
    Ruby,

    [ItemName("Saltpeter"), ItemDescription("A niter ore."), ItemCategory(ItemCategory.Materials)]
    Saltpeter,

    [ItemName("Sapphire"), ItemDescription("A deep blue gemstone."), ItemCategory(ItemCategory.Materials)]
    Sapphire,

    [ItemName("Demon Eye"), ItemDescription("The probing eye of a demon."), ItemCategory(ItemCategory.Materials)]
    SeekerEye,

    [ItemName("Sharp Razor"), ItemDescription("A razor that has cut through more than hair."), ItemCategory(ItemCategory.Materials)]
    SharpRazor,

    [ItemName("Silk"), ItemDescription("Precious, lustrous cloth made from the cocoon of the silkworm. Prized since ancient times."), ItemCategory(ItemCategory.Materials)]
    Silk,

    [ItemName("Silver"), ItemDescription("A precious metal."), ItemCategory(ItemCategory.Materials)]
    Silver,

    [ItemName("Demon Pelt"), ItemDescription("A loathsome pelt taken from a demon."), ItemCategory(ItemCategory.Materials)]
    Silverwolffur,

    [ItemName("Steel"), ItemDescription("A sturdy alloy made from iron ore that is easy to work with and suited for fine detailing."), ItemCategory(ItemCategory.Materials)]
    Steel,

    [ItemName("Sulfate"), ItemDescription("A liquid acid."), ItemCategory(ItemCategory.Materials)]
    Sulfate,

    [ItemName("Sulfur"), ItemDescription("An ore used in alchemy."), ItemCategory(ItemCategory.Materials)]
    Sulfur,

    [ItemName("Thunderbird Plume"), ItemDescription("A light, yet sturdy feather."), ItemCategory(ItemCategory.Materials)]
    ThunderbirdFeathers,

    [ItemName("Monster Blood"), ItemDescription("The blood of some hellish creature."), ItemCategory(ItemCategory.Materials)]
    Toadfluid,

    [ItemName("Toad Heart"), ItemDescription("The heart of a...well, no need to spell it out."), ItemCategory(ItemCategory.Materials)]
    ToadHeart,

    [ItemName("Toad Webbing"), ItemDescription("The wing membrane of a flying toad."), ItemCategory(ItemCategory.Materials)]
    Toadwing,

    [ItemName("Vespine Stinger"), ItemDescription("A stinger containing deadly venom."), ItemCategory(ItemCategory.Materials)]
    TytaniaPoisonedNeedle,

    [ItemName("Vespine Wing"), ItemDescription("A wing plucked from a wasp-like faerie."), ItemCategory(ItemCategory.Materials)]
    Tytaniawing,

    [ItemName("Walnut"), ItemDescription("An extremely sturdy lumber."), ItemCategory(ItemCategory.Materials)]
    WalnutLumber,

    [ItemName("Fiend Skull"), ItemDescription("A skull embued with dark energy."), ItemCategory(ItemCategory.Materials)]
    Warriorskull,

    [ItemName("Flight Feather"), ItemDescription("An elegant feather."), ItemCategory(ItemCategory.Materials)]
    WindFeathers,

    [ItemName("Lycan Claw"), ItemDescription("A razor-sharp wolf's claw."), ItemCategory(ItemCategory.Materials)]
    Wolfclaw,

    [ItemName("Sinister Pelt"), ItemDescription("The pelt of an unusually cruel demon."), ItemCategory(ItemCategory.Materials)]
    Wolfmanfur,

    [ItemName("Adrasteia"), ItemDescription("A gun whose name means \"inescapable.\""), ItemCategory(ItemCategory.Weapon)]
    Adrastea,

    [ItemName("Ayamur"), ItemDescription("A club once used to slay a wicked sea-god. Effective underwater."), ItemCategory(ItemCategory.Weapon)]
    Aimur,

    [ItemName("Albireo"), ItemDescription("A magical whip found locked away beneath the permafrost."), ItemCategory(ItemCategory.Weapon)]
    Albireo,

    [ItemName("Andromeda"), ItemDescription("A holy chain that drives off evil."), ItemCategory(ItemCategory.Weapon)]
    Andromeda,

    [ItemName("Areadbhar"), ItemDescription("A spear that produces flames capable of leveling whole cities."), ItemCategory(ItemCategory.Weapon)]
    Aradovar,

    [ItemName("Almace"), ItemDescription("A sword that sunders and freezes at a stroke. "), ItemCategory(ItemCategory.Weapon)]
    Armas,

    [ItemName("Steam Boots"), ItemDescription("Steel boots fitted with a steam engine."), ItemCategory(ItemCategory.Weapon)]
    Assaultsollette,

    [ItemName("Whip"), ItemDescription("A sturdy leather whip."), ItemCategory(ItemCategory.Weapon)]
    Awhip,

    [ItemName("Baselard"), ItemDescription("A standard, double-edged dagger."), ItemCategory(ItemCategory.Weapon)]
    Baselard,

    [ItemName("Battle Boots"), ItemDescription("Combat boots soled in iron plates."), ItemCategory(ItemCategory.Weapon)]
    BattleBoots,

    [ItemName("Beast Killer"), ItemDescription("A whip that brings any beast to bay."), ItemCategory(ItemCategory.Weapon)]
    BeastKiller,

    [ItemName("Betelgeuse"), ItemDescription("A gun with a rifled barrel that increases the weapon's potency."), ItemCategory(ItemCategory.Weapon)]
    Betelgeuse,

    [ItemName("Black Cords"), ItemDescription("A whip woven from raven hair and bound together by a unique spell."), ItemCategory(ItemCategory.Weapon)]
    BlackRockHell,

    [ItemName("Bloodbringer"), ItemDescription("An enchanted sword with the potential to grow stronger."), ItemCategory(ItemCategory.Weapon)]
    BradBlingerLv1,

    [ItemName("Eu's Sword"), ItemDescription("A certain ruler of the universe's sword. Its power has faded."), ItemCategory(ItemCategory.Weapon)]
    BradeOfEU,

    [ItemName("Eu's Sword (Level 2)"), ItemDescription("A certain ruler of the universe's sword. Its power has faded."), ItemCategory(ItemCategory.Weapon)]
    BradeOfEU2,

    [ItemName("Eu's Sword (Level 3)"), ItemDescription("A certain ruler of the universe's sword. Its power has faded."), ItemCategory(ItemCategory.Weapon)]
    BradeOfEU3,

    [ItemName("Blunderbuss"), ItemDescription("A gun with a wide muzzle akin to the mouth of a trumpet."), ItemCategory(ItemCategory.Weapon)]
    Branderbus,

    [ItemName("Blue Rose"), ItemDescription("A Shardbinder's blade that represents the finest in alchemy."), ItemCategory(ItemCategory.Weapon)]
    Broadsword,

    [ItemName("Blutgang"), ItemDescription("A precious sword used by a legendary defender and champion."), ItemCategory(ItemCategory.Weapon)]
    Burtgang,

    [ItemName("Carnwennan"), ItemDescription("A holy dagger once wielded by an ancient king."), ItemCategory(ItemCategory.Weapon)]
    Calnwenan,

    [ItemName("Caladbolg"), ItemDescription("A blade mantled in lightning."), ItemCategory(ItemCategory.Weapon)]
    Caradoborg,

    [ItemName("Culverin"), ItemDescription("A small ancestor of the musket used by cavalry."), ItemCategory(ItemCategory.Weapon)]
    Carvalin,

    [ItemName("Kazikli"), ItemDescription("A thrusting weapon caked with blood from the many fiends it has impaled."), ItemCategory(ItemCategory.Weapon)]
    Cazilk,

    [ItemName("Claw of Onmoraki"), ItemDescription("A weapon forged by souls seeking purification."), ItemCategory(ItemCategory.Weapon)]
    ChargeWideEnd,

    [ItemName("Claw of Onmoraki (Level 2)"), ItemDescription("A weapon forged by souls seeking purification."), ItemCategory(ItemCategory.Weapon)]
    ChargeWideEnd2,

    [ItemName("Claw of Onmoraki (Level 3)"), ItemDescription("A weapon forged by souls seeking purification."), ItemCategory(ItemCategory.Weapon)]
    ChargeWideEnd3,

    [ItemName("Sharur"), ItemDescription("An icy club that was discovered in the permafrost."), ItemCategory(ItemCategory.Weapon)]
    Charwell,

    [ItemName("Claymore"), ItemDescription("A common, two-handed greatsword."), ItemCategory(ItemCategory.Weapon)]
    Claymore,

    [ItemName("Final Hour"), ItemDescription("A sword forged from the hand of a clock tower."), ItemCategory(ItemCategory.Weapon)]
    ClockSowrd,

    [ItemName("Sword Of Mathildis"), ItemDescription("Will certainly attack Defends, then sends damage back"), ItemCategory(ItemCategory.Weapon)]
    COL_SwordOfMathildis,

    [ItemName("Sword Of Mathildis (Level 2)"), ItemDescription("Slices through evil's veil.  Counterattacks when assailed"), ItemCategory(ItemCategory.Weapon)]
    COL_Zangetsuto,

    [ItemName("Leng Yan Ju"), ItemDescription("The crescent blade a general once used to take on whole armies alone."), ItemCategory(ItemCategory.Weapon)]
    Coldgrindingsaw,

    [ItemName("Blue Skies"), ItemDescription("Blood-soaked boots that hit with all the force of the falling sky."), ItemCategory(ItemCategory.Weapon)]
    CoolShoesOfMrNarita,

    [ItemName("Blue Skies (Level 2)"), ItemDescription("Blood-soaked boots that hit with all the force of the falling sky."), ItemCategory(ItemCategory.Weapon)]
    CoolShoesOfMrNarita2,

    [ItemName("Blue Skies (Level 3)"), ItemDescription("Blood-soaked boots that hit with all the force of the falling sky."), ItemCategory(ItemCategory.Weapon)]
    CoolShoesOfMrNarita3,

    [ItemName("Redbeast's Edge"), ItemDescription("A mysterious, broken longsword fused together with the magi-crystal curse."), ItemCategory(ItemCategory.Weapon)]
    CrystalSword,

    [ItemName("Redbeast's Edge (Level 2)"), ItemDescription("A mysterious, broken longsword fused together with the magi-crystal curse."), ItemCategory(ItemCategory.Weapon)]
    CrystalSword2,

    [ItemName("Redbeast's Edge (Level 3)"), ItemDescription("A mysterious, broken longsword fused together with the magi-crystal curse."), ItemCategory(ItemCategory.Weapon)]
    CrystalSword3,

    [ItemName("Deathbringer"), ItemDescription("Crafted by an orc warlord. Forged in hellfire."), ItemCategory(ItemCategory.Weapon)]
    DeathBringer,

    [ItemName("Deathbringer (Level 2)"), ItemDescription("Crafted by an orc warlord. Forged in hellfire."), ItemCategory(ItemCategory.Weapon)]
    DeathBringer2,

    [ItemName("Deathbringer (Level 3)"), ItemDescription("Crafted by an orc warlord. Forged in hellfire."), ItemCategory(ItemCategory.Weapon)]
    DeathBringer3,

    [ItemName("Hell's Knells"), ItemDescription("Shoes that a demon created with the intent of beheading God himself."), ItemCategory(ItemCategory.Weapon)]
    Decapitator,

    [ItemName("Dies Irae"), ItemDescription("A greatsword that leads victims to their demise."), ItemCategory(ItemCategory.Weapon)]
    DiesIle,

    [ItemName("Dáinsleif"), ItemDescription("A magical blade that thirsts for the blood of the living."), ItemCategory(ItemCategory.Weapon)]
    Dineslave,

    [ItemName("Dragon Shoes"), ItemDescription("Combat boots once prized by a master of the martial arts."), ItemCategory(ItemCategory.Weapon)]
    Dragonshoes,

    [ItemName("Blood Grinder"), ItemDescription("A cruel and bloody sword of death. Merciless upon the flesh."), ItemCategory(ItemCategory.Weapon)]
    DrillWideEnd,

    [ItemName("Blood Grinder (Level 2)"), ItemDescription("A cruel and bloody sword of death. Merciless upon the flesh."), ItemCategory(ItemCategory.Weapon)]
    DrillWideEnd2,

    [ItemName("Blood Grinder (Level 3)"), ItemDescription("A cruel and bloody sword of death. Merciless upon the flesh."), ItemCategory(ItemCategory.Weapon)]
    DrillWideEnd3,

    [ItemName("Dull Blade"), ItemDescription("The work of a forgotten swordsmith."), ItemCategory(ItemCategory.Weapon)]
    Dull,

    [ItemName("Dungeonite Sword"), ItemDescription("A Relic from the Dungeon Clan. Carries the Hopes of 1400+ souls."), ItemCategory(ItemCategory.Weapon)]
    DungeonNightSword,

    [ItemName("Durandal"), ItemDescription("A champion's greatsword, capable of sundering all things in the universe."), ItemCategory(ItemCategory.Weapon)]
    Durandal,

    [ItemName("Epée"), ItemDescription("A rapier used in duels."), ItemCategory(ItemCategory.Weapon)]
    Epe,

    [ItemName("Dominus"), ItemDescription("A mysterious blade forged by Operation Akumaclan."), ItemCategory(ItemCategory.Weapon)]
    EvilTheSword,

    [ItemName("Eternal Blue"), ItemDescription("A Shardbinder's greatsword that represents the finest in alchemy."), ItemCategory(ItemCategory.Weapon)]
    Exactor,

    [ItemName("Shovel"), ItemDescription("A shovel that functions as a weapon."), ItemCategory(ItemCategory.Weapon)]
    Excavator,

    [ItemName("Flame Whip"), ItemDescription("A whip that leaves an arc of fire in its wake."), ItemCategory(ItemCategory.Weapon)]
    FireWhip,

    [ItemName("Fragarach"), ItemDescription("A holy sword that answers to its master's will."), ItemCategory(ItemCategory.Weapon)]
    Flagarach,

    [ItemName("Flamberge"), ItemDescription("A greatsword with a wavy blade designed to deal  fatal blows."), ItemCategory(ItemCategory.Weapon)]
    Flanders,

    [ItemName("Florenberg"), ItemDescription("A sword imbued with fire and courage. "), ItemCategory(ItemCategory.Weapon)]
    Florenberg,

    [ItemName("Gambanteinn"), ItemDescription("An enchanted staff that grants resistance to magic."), ItemCategory(ItemCategory.Weapon)]
    Gambanttain,

    [ItemName("Gae Assail"), ItemDescription("A spear of light that purifies the soul of whomever it pierces."), ItemCategory(ItemCategory.Weapon)]
    Gayalsals,

    [ItemName("Gram"), ItemDescription("A hero's greatsword. Answers only to those it deems worthy."), ItemCategory(ItemCategory.Weapon)]
    Gram,

    [ItemName("Gungnir"), ItemDescription("A thunder god's spear. It always strikes its target."), ItemCategory(ItemCategory.Weapon)]
    GunungNil,

    [ItemName("Harpé"), ItemDescription("A sword with a scythe-like blade."), ItemCategory(ItemCategory.Weapon)]
    Halper,

    [ItemName("Hermes Shoes"), ItemDescription("Shoes imbued with a wind spirit's power."), ItemCategory(ItemCategory.Weapon)]
    Hermesshoes,

    [ItemName("Hofud"), ItemDescription("A storied blade that has cracked the skulls of myriad adversaries."), ItemCategory(ItemCategory.Weapon)]
    Hoffdo,

    [ItemName("Bandit Blade"), ItemDescription("A sword with hidden capabilities."), ItemCategory(ItemCategory.Weapon)]
    HuntedBrad,

    [ItemName("Thorn Whip"), ItemDescription("A whip lined with thorn-like spikes."), ItemCategory(ItemCategory.Weapon)]
    Ibarakaswhip,

    [ItemName("Sculptor's Chisel"), ItemDescription("An ice lance. Sadly, not a sickle. Chisels away all of your enemies."), ItemCategory(ItemCategory.Weapon)]
    IcePillarSpear,

    [ItemName("Sculptor's Chisel (Level 2)"), ItemDescription("An ice lance. Sadly, not a sickle. Chisels away all of your enemies."), ItemCategory(ItemCategory.Weapon)]
    IcePillarSpear2,

    [ItemName("Sculptor's Chisel (Level 3)"), ItemDescription("An ice lance. Sadly, not a sickle. Chisels away all of your enemies."), ItemCategory(ItemCategory.Weapon)]
    IcePillarSpear3,

    [ItemName("Boreal Rime Boots"), ItemDescription("Boots imbued with an ice deity. Used by a heroine who saved her homeland."), ItemCategory(ItemCategory.Weapon)]
    IceSlewShoes,

    [ItemName("Boreal Rime Boots (Level 2)"), ItemDescription("Boots imbued with an ice deity. Used by a heroine who saved her homeland."), ItemCategory(ItemCategory.Weapon)]
    IceSlewShoes2,

    [ItemName("Boreal Rime Boots (Level 3)"), ItemDescription("Boots imbued with an ice deity. Used by a heroine who saved her homeland."), ItemCategory(ItemCategory.Weapon)]
    IceSlewShoes3,

    [ItemName("Invisible Blade"), ItemDescription("A blade that has been made invisible through  sorcery."), ItemCategory(ItemCategory.Weapon)]
    invisible,

    [ItemName("Hikari"), ItemDescription("A sword that purifies evil with its divine light."), ItemCategory(ItemCategory.Weapon)]
    JodoSwordLight,

    [ItemName("Hikari (Level 2)"), ItemDescription("A sword that purifies evil with its divine light."), ItemCategory(ItemCategory.Weapon)]
    JodoSwordLight2,

    [ItemName("Hikari (Level 3)"), ItemDescription("A sword that purifies evil with its divine light."), ItemCategory(ItemCategory.Weapon)]
    JodoSwordLight3,

    [ItemName("Joyeuse"), ItemDescription("A treasured sword that shines with all the colors of the rainbow."), ItemCategory(ItemCategory.Weapon)]
    Juwuse,

    [ItemName("Kaladanda"), ItemDescription("A staff the lord of the underworld used to preside over the dead."), ItemCategory(ItemCategory.Weapon)]
    Karadanda,

    [ItemName("Lethal Boots"), ItemDescription("An assassin's boots. There are knives concealed inside."), ItemCategory(ItemCategory.Weapon)]
    KillerBoots,

    [ItemName("Knife"), ItemDescription("A knife for self-defense."), ItemCategory(ItemCategory.Weapon)]
    Knife,

    [ItemName("Clockwork Blade"), ItemDescription("Forged from the gears of an ancient clock."), ItemCategory(ItemCategory.Weapon)]
    KongSword,

    [ItemName("Kukri"), ItemDescription("A knife with an inwardly curved blade."), ItemCategory(ItemCategory.Weapon)]
    Kukuri,

    [ItemName("Kung Fu Shoes"), ItemDescription("Shoes from the Far East that were designed not to encumber movement."), ItemCategory(ItemCategory.Weapon)]
    KungFuShoes,

    [ItemName("Ridill"), ItemDescription("A sword once used to remove a dragon's heart."), ItemCategory(ItemCategory.Weapon)]
    Liddyl,

    [ItemName("Steel Lightning"), ItemDescription("A wondrous greatsword that was struck by lightning and retained its charge."), ItemCategory(ItemCategory.Weapon)]
    LightningBolt,

    [ItemName("Raikiri"), ItemDescription("A blade said to have cut down a thunder god."), ItemCategory(ItemCategory.Weapon)]
    Lightningoff,

    [ItemName("Encrypted Orchid"), ItemDescription("6F20636B6E7077206772646C7A207A77 6B6A207075707365756C2078726B7674"), ItemCategory(ItemCategory.Weapon)]
    LightSaber,

    [ItemName("Encrypted Orchid (Level 2)"), ItemDescription("6F20636B6E7077206772646C7A207A77 6B6A207075707365756C2078726B7674"), ItemCategory(ItemCategory.Weapon)]
    LightSaber2,

    [ItemName("Encrypted Orchid (Level 3)"), ItemDescription("6F20636B6E7077206772646C7A207A77 6B6A207075707365756C2078726B7674"), ItemCategory(ItemCategory.Weapon)]
    LightSaber3,

    [ItemName("Lohengrin"), ItemDescription("A knight's greatsword that was forged in holy fire."), ItemCategory(ItemCategory.Weapon)]
    Lohengrin,

    [ItemName("Long Sword"), ItemDescription("A long sword favored by soldiers."), ItemCategory(ItemCategory.Weapon)]
    LongSword,

    [ItemName("Black Dragon's Ode"), ItemDescription("A weapon made by a black dragon who fell in love with a faerie."), ItemCategory(ItemCategory.Weapon)]
    LoveOfFairyDragon,

    [ItemName("Black Dragon's Ode (Level 2)"), ItemDescription("A weapon made by a black dragon who fell in love with a faerie."), ItemCategory(ItemCategory.Weapon)]
    LoveOfFairyDragon2,

    [ItemName("Black Dragon's Ode (Level 3)"), ItemDescription("A weapon made by a black dragon who fell in love with a faerie."), ItemCategory(ItemCategory.Weapon)]
    LoveOfFairyDragon3,

    [ItemName("Sicilian Slicer"), ItemDescription("Only weirdoughs don't want a pizza this weapon... too cheesy?"), ItemCategory(ItemCategory.Weapon)]
    LoveOfPizza,

    [ItemName("Mace"), ItemDescription("A type of club used in war."), ItemCategory(ItemCategory.Weapon)]
    Mace,

    [ItemName("Miri Scepter"), ItemDescription("Tap attack to wave this wonderful wand. Hold attack to fire beams of magical light."), ItemCategory(ItemCategory.Weapon)]
    MagicalScepter,

    [ItemName("Miri Scepter (Level 2)"), ItemDescription("Tap attack to wave this wonderful wand. Hold attack to fire beams of magical light."), ItemCategory(ItemCategory.Weapon)]
    MagicalScepter1,

    [ItemName("Miri Scepter (Level 3)"), ItemDescription("Tap attack to wave this wonderful wand. Hold attack to fire beams of magical light."), ItemCategory(ItemCategory.Weapon)]
    MagicalScepter2,

    [ItemName("Miri Scepter (Level 4)"), ItemDescription("Tap attack to wave this wonderful wand. Hold attack to fire beams of magical light."), ItemCategory(ItemCategory.Weapon)]
    MagicalScepter3,

    [ItemName("Miri Scepter (Level 5)"), ItemDescription("Tap attack to wave this wonderful wand. Hold attack to fire beams of magical light."), ItemCategory(ItemCategory.Weapon)]
    MagicalScepter4,

    [ItemName("Mandau Pasir"), ItemDescription("A large dagger used in headhunting."), ItemCategory(ItemCategory.Weapon)]
    Mandaupasar,

    [ItemName("Main Gauche"), ItemDescription("A dagger useful for both offense and defense."), ItemCategory(ItemCategory.Weapon)]
    ManGauche,

    [ItemName("Macuahuitl"), ItemDescription("A lethal cudgel embedded with obsidian blades."), ItemCategory(ItemCategory.Weapon)]
    McAutil,

    [ItemName("Mikazuki"), ItemDescription("The work of the swordsmith Munechika. It boasts a stunning blade pattern."), ItemCategory(ItemCategory.Weapon)]
    MikazukiMaen,

    [ItemName("Misericorde"), ItemDescription("A thrusting weapon known as the \"sword of mercy.\" "), ItemCategory(ItemCategory.Weapon)]
    Miserikorde,

    [ItemName("Mistilteinn"), ItemDescription("A holy staff carved from a sacred tree."), ItemCategory(ItemCategory.Weapon)]
    Mistrutein,

    [ItemName("Morgenstern"), ItemDescription("A spiked mace that somewhat resembles a star."), ItemCategory(ItemCategory.Weapon)]
    Morgenstern,

    [ItemName("Murgleis"), ItemDescription("A cursed sword that has been known to turn  enemies to stone."), ItemCategory(ItemCategory.Weapon)]
    Mulgres,

    [ItemName("Musketoon"), ItemDescription("A short-barreled gun used by pirates."), ItemCategory(ItemCategory.Weapon)]
    Musketon,

    [ItemName("Niflheim"), ItemDescription("A spear of ice that was forged in hell."), ItemCategory(ItemCategory.Weapon)]
    Nibbleheim,

    [ItemName("Nodachi"), ItemDescription("A long blade used by cavalry in the Far East."), ItemCategory(ItemCategory.Weapon)]
    Noda,

    [ItemName("Swordfish"), ItemDescription("A giant, wooden sword named after a fish."), ItemCategory(ItemCategory.Weapon)]
    OgreWoodenSword,

    [ItemName("Oracle Blade"), ItemDescription("An improved flying edge."), ItemCategory(ItemCategory.Weapon)]
    OracleBlade,

    [ItemName("Spiral Sword"), ItemDescription("A large sword with a blade that rotates like a drill."), ItemCategory(ItemCategory.Weapon)]
    OutsiderKnightSword,

    [ItemName("Partisan"), ItemDescription("A lance with a large, broad blade."), ItemCategory(ItemCategory.Weapon)]
    Partizan,

    [ItemName("Pelekus"), ItemDescription("A double-bladed greataxe originally designed for harvesting wood."), ItemCategory(ItemCategory.Weapon)]
    Perex,

    [ItemName("Silent Calamity"), ItemDescription("A weapon that crushes opponents and ushers in silence."), ItemCategory(ItemCategory.Weapon)]
    PetrifactionSword,

    [ItemName("Silent Calamity (Level 2)"), ItemDescription("A weapon that crushes opponents and ushers in silence."), ItemCategory(ItemCategory.Weapon)]
    PetrifactionSword2,

    [ItemName("Silent Calamity (Level 3)"), ItemDescription("A weapon that crushes opponents and ushers in silence."), ItemCategory(ItemCategory.Weapon)]
    PetrifactionSword3,

    [ItemName("Risky Pistol"), ItemDescription("A cursed flintlock pistol once owned by a master pirate."), ItemCategory(ItemCategory.Weapon)]
    PirateGun1,

    [ItemName("Risky Pistol (Level 2)"), ItemDescription("A cursed flintlock pistol once owned by a master pirate."), ItemCategory(ItemCategory.Weapon)]
    PirateGun2,

    [ItemName("Risky Pistol (Level 3)"), ItemDescription("A cursed flintlock pistol once owned by a master pirate."), ItemCategory(ItemCategory.Weapon)]
    PirateGun3,

    [ItemName("Risky Pistol (Level 4)"), ItemDescription("A cursed flintlock pistol once owned by a master pirate."), ItemCategory(ItemCategory.Weapon)]
    PirateGun4,

    [ItemName("Risky Pistol (Level 5)"), ItemDescription("A cursed flintlock pistol once owned by a master pirate."), ItemCategory(ItemCategory.Weapon)]
    PirateGun5,

    [ItemName("Risky Blade"), ItemDescription("A cursed scimitar suitable for a pirate queen!"), ItemCategory(ItemCategory.Weapon)]
    PirateSword1,

    [ItemName("Risky Blade (Level 2)"), ItemDescription("A cursed scimitar suitable for a pirate queen!"), ItemCategory(ItemCategory.Weapon)]
    PirateSword2,

    [ItemName("Risky Blade (Level 3)"), ItemDescription("A cursed scimitar suitable for a pirate queen!"), ItemCategory(ItemCategory.Weapon)]
    PirateSword3,

    [ItemName("Risky Blade (Level 4)"), ItemDescription("A cursed scimitar suitable for a pirate queen!"), ItemCategory(ItemCategory.Weapon)]
    PirateSword4,

    [ItemName("Risky Blade (Level 5)"), ItemDescription("A cursed scimitar suitable for a pirate queen!"), ItemCategory(ItemCategory.Weapon)]
    PirateSword5,

    [ItemName("Poison Kukri"), ItemDescription("A curved knife that has been laced with a deadly poison."), ItemCategory(ItemCategory.Weapon)]
    PoisonKukri,

    [ItemName("Oleanders"), ItemDescription("Alchemical shoes designed for guests both welcome and not."), ItemCategory(ItemCategory.Weapon)]
    PoisonSpikeShoes,

    [ItemName("Oleanders (Level 2)"), ItemDescription("Alchemical shoes designed for guests both welcome and not."), ItemCategory(ItemCategory.Weapon)]
    PoisonSpikeShoes2,

    [ItemName("Oleanders (Level 3)"), ItemDescription("Alchemical shoes designed for guests both welcome and not."), ItemCategory(ItemCategory.Weapon)]
    PoisonSpikeShoes3,

    [ItemName("Game Sack Strip"), ItemDescription("A ridiculously branded power strip of many outlets."), ItemCategory(ItemCategory.Weapon)]
    PowerSword,

    [ItemName("Bunny Boots"), ItemDescription("Adorable boots fashioned after a rabbit."), ItemCategory(ItemCategory.Weapon)]
    Rabbitboots,

    [ItemName("Rapier"), ItemDescription("A single-handed sword designed for thrusting."), ItemCategory(ItemCategory.Weapon)]
    Rapier,

    [ItemName("Red Umbrella"), ItemDescription("An umbrella that blood-rain has stained a deep red. "), ItemCategory(ItemCategory.Weapon)]
    Redumbrella,

    [ItemName("Lance"), ItemDescription("A lance used primarily on horseback."), ItemCategory(ItemCategory.Weapon)]
    Reims,

    [ItemName("Flying Edge"), ItemDescription("A blade that has been magically endowed with flight capabilities."), ItemCategory(ItemCategory.Weapon)]
    RemoteDart,

    [ItemName("Dawn Blade"), ItemDescription("A greatsword containing the power of dawn."), ItemCategory(ItemCategory.Weapon)]
    SacredSword,

    [ItemName("Dawn Blade (Level 2)"), ItemDescription("A greatsword containing the power of dawn."), ItemCategory(ItemCategory.Weapon)]
    SacredSword2,

    [ItemName("Dawn Blade (Level 3)"), ItemDescription("A greatsword containing the power of dawn."), ItemCategory(ItemCategory.Weapon)]
    SacredSword3,

    [ItemName("Scythe"), ItemDescription("A curved blade to reap while they sleep."), ItemCategory(ItemCategory.Weapon)]
    Scythe,

    [ItemName("Scythe (Level 2)"), ItemDescription("A curved blade to reap while they sleep."), ItemCategory(ItemCategory.Weapon)]
    Scythe1,

    [ItemName("Scythe (Level 3)"), ItemDescription("A curved blade to reap while they sleep."), ItemCategory(ItemCategory.Weapon)]
    Scythe2,

    [ItemName("Scythe (Level 4)"), ItemDescription("A curved blade to reap while they sleep."), ItemCategory(ItemCategory.Weapon)]
    Scythe3,

    [ItemName("Scythe (Level 5)"), ItemDescription("A curved blade to reap while they sleep."), ItemCategory(ItemCategory.Weapon)]
    Scythe4,

    [ItemName("Schedar"), ItemDescription("A greatsword imbued with the power of ice."), ItemCategory(ItemCategory.Weapon)]
    Sherdar,

    [ItemName("Shield Weapon"), ItemDescription("A weapon that functions as a shield."), ItemCategory(ItemCategory.Weapon)]
    ShieldWeapon,

    [ItemName("Shield Weapon (Level 2)"), ItemDescription("A weapon that functions as a shield."), ItemCategory(ItemCategory.Weapon)]
    ShieldWeapon2,

    [ItemName("Shield Weapon (Level 3)"), ItemDescription("A weapon that functions as a shield."), ItemCategory(ItemCategory.Weapon)]
    ShieldWeapon3,

    [ItemName("Gondo-Shizunori"), ItemDescription("The famous spear wielded by a valiant Eastern general."), ItemCategory(ItemCategory.Weapon)]
    ShingoGempo,

    [ItemName("Honebami"), ItemDescription("A Nipponese halberd refashioned into a sword by the swordsmith Toshiro."), ItemCategory(ItemCategory.Weapon)]
    ShiroTorujiro,

    [ItemName("Short Sword"), ItemDescription("A short sword favored by soldiers."), ItemCategory(ItemCategory.Weapon)]
    ShortSword,

    [ItemName("Darkness Descends"), ItemDescription("Death is certain, life is not."), ItemCategory(ItemCategory.Weapon)]
    SilverAndBlackSword,

    [ItemName("Snakebite"), ItemDescription("A poison-laced whip fashioned after a serpent's fangs."), ItemCategory(ItemCategory.Weapon)]
    Snakebyte,

    [ItemName("Spear"), ItemDescription("A light spear."), ItemCategory(ItemCategory.Weapon)]
    Spear,

    [ItemName("Moonwake"), ItemDescription("Moonlight shaped into a blade Reflecting the heart's true self"), ItemCategory(ItemCategory.Weapon)]
    SpearCutDownAside,

    [ItemName("Moonwake (Level 2)"), ItemDescription("Moonlight shaped into a blade Reflecting the heart's true self"), ItemCategory(ItemCategory.Weapon)]
    SpearCutDownAside2,

    [ItemName("Moonwake (Level 3)"), ItemDescription("Moonlight shaped into a blade Reflecting the heart's true self"), ItemCategory(ItemCategory.Weapon)]
    SpearCutDownAside3,

    [ItemName("Carnot's Rebuke "), ItemDescription("A greatsword whose internal steam engine adds incredible power to each swing."), ItemCategory(ItemCategory.Weapon)]
    SteamFlatWideEnd,

    [ItemName("Prismatic Heart"), ItemDescription("A strange rod from another world. The jewel emits a magical light."), ItemCategory(ItemCategory.Weapon)]
    StickOfMagiGirl,

    [ItemName("Prismatic Heart (Level 2)"), ItemDescription("A strange rod from another world. The jewel emits a magical light."), ItemCategory(ItemCategory.Weapon)]
    StickOfMagiGirl2,

    [ItemName("Prismatic Heart (Level 3)"), ItemDescription("A strange rod from another world. The jewel emits a magical light."), ItemCategory(ItemCategory.Weapon)]
    StickOfMagiGirl3,

    [ItemName("Stinger"), ItemDescription("A thrusting weapon modeled after a bee's stinger and tipped with poison."), ItemCategory(ItemCategory.Weapon)]
    Stinger,

    [ItemName("Swordbreaker"), ItemDescription("A dagger for damaging opponents' weapons. Lowers enemy attack power."), ItemCategory(ItemCategory.Weapon)]
    Swordbreaker,

    [ItemName("Vine Sword"), ItemDescription("A sword of vines that utilizes an ancient corruption magic."), ItemCategory(ItemCategory.Weapon)]
    SwordOfTheMushroom,

    [ItemName("Zangetsuto"), ItemDescription("A great blade whose bearers are given the name Zangetsu, or \"moon sunderer.\""), ItemCategory(ItemCategory.Weapon)]
    Swordsman,

    [ItemName("Sword Whip"), ItemDescription("A sword with a flexible blade reminiscent of a whip."), ItemCategory(ItemCategory.Weapon)]
    SwordWhip,

    [ItemName("Dojigiri"), ItemDescription("A great blade by the smith Yasutsuna. It appears in an oni-slaying tale."), ItemCategory(ItemCategory.Weapon)]
    Tadanako,

    [ItemName("Tanegashima"), ItemDescription("A short-barreled gun used in the Far East."), ItemCategory(ItemCategory.Weapon)]
    Tanegasima,

    [ItemName("Toy Shoes"), ItemDescription("Shoes that emit an adorable squeak with each step."), ItemCategory(ItemCategory.Weapon)]
    Toyshoes,

    [ItemName("Toradar"), ItemDescription("A long matchlock used for picking off targets at a distance."), ItemCategory(ItemCategory.Weapon)]
    Trador,

    [ItemName("Sanjiegun"), ItemDescription("A special three-section staff with extendible joints."), ItemCategory(ItemCategory.Weapon)]
    Triplet,

    [ItemName("Grand Izayoi"), ItemDescription("A blade made at great personal risk. It grows and has a mind of its own."), ItemCategory(ItemCategory.Weapon)]
    Truesixteenthnight,

    [ItemName("Renee's Requiem"), ItemDescription("\"With her last breath, she imbues this weapon with her soul.\""), ItemCategory(ItemCategory.Weapon)]
    TrustMusket,

    [ItemName("Renee's Requiem (Level 2)"), ItemDescription("\"With her last breath, she imbues this weapon with her soul.\""), ItemCategory(ItemCategory.Weapon)]
    TrustMusket2,

    [ItemName("Renee's Requiem (Level 3)"), ItemDescription("\"With her last breath, she imbues this weapon with her soul.\""), ItemCategory(ItemCategory.Weapon)]
    TrustMusket3,

    [ItemName("Tsurumaru"), ItemDescription("A blade with an elegant curve. The work of the swordsmith Kuninaga."), ItemCategory(ItemCategory.Weapon)]
    Tsurumaru,

    [ItemName("Ukonvasara"), ItemDescription("A thunder god's axe."), ItemCategory(ItemCategory.Weapon)]
    TurmericBasara,

    [ItemName("Verethragna"), ItemDescription("A gun that grants victory to whoever holds it."), ItemCategory(ItemCategory.Weapon)]
    Ursula,

    [ItemName("Berdiche"), ItemDescription("A greataxe destructive enough to split boulders in twain."), ItemCategory(ItemCategory.Weapon)]
    Valdish,

    [ItemName("Valkyrie Sword"), ItemDescription("A warmaiden's weapon of choice."), ItemCategory(ItemCategory.Weapon)]
    ValkyrieSword,

    [ItemName("Rhava Velar"), ItemDescription("A sword that uses divine power to generate an eviscerating wind."), ItemCategory(ItemCategory.Weapon)]
    ValralAltar,

    [ItemName("Japanesque Umbrella"), ItemDescription("When closed, a traditional parasol. Hold attack to open, creating a bullet-blocking peril-saw."), ItemCategory(ItemCategory.Weapon)]
    Wagasa,

    [ItemName("Japanesque Umbrella (Level 2)"), ItemDescription("When closed, a traditional parasol. Hold attack to open, creating a bullet-blocking peril-saw."), ItemCategory(ItemCategory.Weapon)]
    Wagasa1,

    [ItemName("Japanesque Umbrella (Level 3)"), ItemDescription("When closed, a traditional parasol. Hold attack to open, creating a bullet-blocking peril-saw."), ItemCategory(ItemCategory.Weapon)]
    Wagasa2,

    [ItemName("Japanesque Umbrella (Level 4)"), ItemDescription("When closed, a traditional parasol. Hold attack to open, creating a bullet-blocking peril-saw."), ItemCategory(ItemCategory.Weapon)]
    Wagasa3,

    [ItemName("Japanesque Umbrella (Level 5)"), ItemDescription("When closed, a traditional parasol. Hold attack to open, creating a bullet-blocking peril-saw."), ItemCategory(ItemCategory.Weapon)]
    Wagasa4,

    [ItemName("Rhava Búral"), ItemDescription("A sword that uses a wind spirit's power to rip foes apart."), ItemCategory(ItemCategory.Weapon)]
    WalalSoulimo,

    [ItemName("Ambivalence"), ItemDescription("A whip bound by powers of darkness. Charge it to release its light."), ItemCategory(ItemCategory.Weapon)]
    WhipsOfLightDarkness,

    [ItemName("Ambivalence (Level 2)"), ItemDescription("A whip bound by powers of darkness. Charge it to release its light."), ItemCategory(ItemCategory.Weapon)]
    WhipsOfLightDarkness2,

    [ItemName("Ambivalence (Level 3)"), ItemDescription("A whip bound by powers of darkness. Charge it to release its light."), ItemCategory(ItemCategory.Weapon)]
    WhipsOfLightDarkness3,

    [ItemName("Ulfberht Sword"), ItemDescription("A longsword favored by Vikings."), ItemCategory(ItemCategory.Weapon)]
    WolfBalt,

    [ItemName("Gold Cross"), ItemDescription("A sword imbued with strange powers. Originally created by a secretive brotherhood, gives strength in times of need."), ItemCategory(ItemCategory.Weapon)]
    XrossBrade,

    [ItemName("Gold Cross (Level 2)"), ItemDescription("A sword imbued with strange powers. Originally created by a secretive brotherhood, gives strength in times of need."), ItemCategory(ItemCategory.Weapon)]
    XrossBrade2,

    [ItemName("Gold Cross (Level 3)"), ItemDescription("A sword imbued with strange powers. Originally created by a secretive brotherhood, gives strength in times of need."), ItemCategory(ItemCategory.Weapon)]
    XrossBrade3,

    [ItemName("Yagrush "), ItemDescription("A thunderous club once wielded by a storm god."), ItemCategory(ItemCategory.Weapon)]
    Yagurushi,

    [ItemName("Vampiric Wig"), ItemDescription("Headwear to haunt men's dreams."), ItemCategory(ItemCategory.HeadArmor)]
    AlluringHorns,

    [ItemName("Aries' Horns"), ItemDescription("A hair accessory that resembles ram horns.  Miriam's favorite."), ItemCategory(ItemCategory.HeadArmor)]
    AriesHorns,

    [ItemName("Beast Beret"), ItemDescription("A cap fashioned from leather."), ItemCategory(ItemCategory.HeadArmor)]
    Beastberet,

    [ItemName("Beret of Bravery"), ItemDescription("A cap that grants the wearer courage in the face of difficulty."), ItemCategory(ItemCategory.HeadArmor)]
    BraveBeret,

    [ItemName("Bunny Ears"), ItemDescription("An accessory that adorns the wearer with the ears of a rabbit."), ItemCategory(ItemCategory.HeadArmor)]
    BunnyEars,

    [ItemName("Cat Ears"), ItemDescription("An adorable accessory that gives the wearer cat ears."), ItemCategory(ItemCategory.HeadArmor)]
    CatEars,

    [ItemName("Circlet"), ItemDescription("A crown encrusted with cheap jewels."), ItemCategory(ItemCategory.HeadArmor)]
    Circlet,

    [ItemName("Flame Circlet"), ItemDescription("A circlet under the protection of fire."), ItemCategory(ItemCategory.HeadArmor)]
    Circletoffire,

    [ItemName("Cowboy Hat"), ItemDescription("A hat worn by cowboys. Increases the power of firearms."), ItemCategory(ItemCategory.HeadArmor)]
    CowboyHat,

    [ItemName("Crow Hat"), ItemDescription("A jet black hat."), ItemCategory(ItemCategory.HeadArmor)]
    Crowhat,

    [ItemName("Cute Beast Beret"), ItemDescription("A cap made to resemble an adorable puppy."), ItemCategory(ItemCategory.HeadArmor)]
    Cutebeastberet,

    [ItemName("Demon Horns"), ItemDescription("A hair accessory inspired by demon horns."), ItemCategory(ItemCategory.HeadArmor)]
    DemonHorns,

    [ItemName("Diabolist's Cap"), ItemDescription("A hat worn by Eastern necromancers."), ItemCategory(ItemCategory.HeadArmor)]
    Dentistshat,

    [ItemName("Dullahammer Helm"), ItemDescription("A dullahammer head that has been refashioned into  human armor."), ItemCategory(ItemCategory.HeadArmor)]
    DurahanMaherm,

    [ItemName("Ancient Tiara"), ItemDescription("A crown passed down since times of yore."), ItemCategory(ItemCategory.HeadArmor)]
    ElderTiara,

    [ItemName("Feather Crown"), ItemDescription("A hair accessory adorned with beautiful plumage."), ItemCategory(ItemCategory.HeadArmor)]
    FeatherCrown,

    [ItemName("Traveler's Hat"), ItemDescription("A wide-brimmed hat that is ideal for extended journeys."), ItemCategory(ItemCategory.HeadArmor)]
    Flightcapoftraveler,

    [ItemName("Gadget Band"), ItemDescription("A hairband fitted with gadgetry."), ItemCategory(ItemCategory.HeadArmor)]
    GadgetBand,

    [ItemName("Garbo Hat"), ItemDescription("An elegant hat with a billowed brim."), ItemCategory(ItemCategory.HeadArmor)]
    Garbohat,

    [ItemName("Hairband"), ItemDescription("A simple hairband that keeps hair away from the  wearer's eyes."), ItemCategory(ItemCategory.HeadArmor)]
    Headband,

    [ItemName("Hermit's Beret"), ItemDescription("A cap that helps mask the wearer's presence."), ItemCategory(ItemCategory.HeadArmor)]
    HermitsBeret,

    [ItemName("Guardian Egg Helm"), ItemDescription("A helm bearing the visage of the beloved patron saint of eggs."), ItemCategory(ItemCategory.HeadArmor)]
    HumptyDumpty,

    [ItemName("Ice Circlet"), ItemDescription("A circlet under the protection of ice."), ItemCategory(ItemCategory.HeadArmor)]
    Icecirclet,

    [ItemName("Japanesque Wig"), ItemDescription("A traditional decorative hairpin."), ItemCategory(ItemCategory.HeadArmor)]
    Kanzashi,

    [ItemName("Kitsune Mask"), ItemDescription("A gift from a fox spirit that holds supernatural powers."), ItemCategory(ItemCategory.HeadArmor)]
    KitsuneMask,

    [ItemName("Dumping Helmet"), ItemDescription("The ideal helmet for times When they scare it out of you."), ItemCategory(ItemCategory.HeadArmor)]
    LoveOfBenki,

    [ItemName("Miri Wig"), ItemDescription("Genki accessories that trail behind Justice Warrior Miri as she battles evil forces."), ItemCategory(ItemCategory.HeadArmor)]
    MagicalGirlHead,

    [ItemName("Witch's Hat"), ItemDescription("A witch's hat that aids concentration and hastens  magic recovery."), ItemCategory(ItemCategory.HeadArmor)]
    Magushat,

    [ItemName("Maid's Hairband"), ItemDescription("A hairband with cute frills that can only be described as \"maid to order.\" "), ItemCategory(ItemCategory.HeadArmor)]
    Maidheadband,

    [ItemName("Mega64 Helmet"), ItemDescription("A powerful helmet made by a sinister scientist."), ItemCategory(ItemCategory.HeadArmor)]
    Mega64Head,

    [ItemName("Crown of Creation"), ItemDescription("Uses Gold to reduce damage. With proper training, allows one to secure a distant kingdom."), ItemCategory(ItemCategory.HeadArmor)]
    MonarchCrown,

    [ItemName("Pirate Hat"), ItemDescription("A hat worn by pirates."), ItemCategory(ItemCategory.HeadArmor)]
    PirateTriangleCap,

    [ItemName("Space Helmet"), ItemDescription("A fish bowl that got stuck on your head."), ItemCategory(ItemCategory.HeadArmor)]
    Plan9FOSpace,

    [ItemName("Pleiades"), ItemDescription("A precious crown adorned with jewels arranged like the stars."), ItemCategory(ItemCategory.HeadArmor)]
    Pleiades,

    [ItemName("Recycle Hat"), ItemDescription("A strange hat that stops ammunition from depleting."), ItemCategory(ItemCategory.HeadArmor)]
    Recyclehat,

    [ItemName("Ribbon"), ItemDescription("A hair accessory reminiscent of butterfly wings."), ItemCategory(ItemCategory.HeadArmor)]
    Ribbon,

    [ItemName("Santa Hat"), ItemDescription("Just what you need to get into the Christmas spirit."), ItemCategory(ItemCategory.HeadArmor)]
    Santacap,

    [ItemName("Risky Bandana"), ItemDescription("A skull cap for your skull cap."), ItemCategory(ItemCategory.HeadArmor)]
    ShantaeBandana,

    [ItemName("Silver Tiara"), ItemDescription("A stunning piece of silverwork."), ItemCategory(ItemCategory.HeadArmor)]
    SilverTiara,

    [ItemName("Thunder Circlet"), ItemDescription("A circlet under the protection of thunder."), ItemCategory(ItemCategory.HeadArmor)]
    Thunderscirclet,

    [ItemName("Valkyrie Tiara"), ItemDescription("A tiara to grace a warmaiden's brow."), ItemCategory(ItemCategory.HeadArmor)]
    ValkyrieTiara,

    [ItemName("Gunslinger's Hat"), ItemDescription("A hat that doubles the ammunition you use but makes firearms stronger."), ItemCategory(ItemCategory.HeadArmor)]
    Westernhat,

    [ItemName("Wolf Hood"), ItemDescription("A hood designed to resemble a wolf."), ItemCategory(ItemCategory.HeadArmor)]
    WolfHood,

    [ItemName("Bat Wings"), ItemDescription("A scarf that resembles bat wings."), ItemCategory(ItemCategory.Scarves)]
    Batwing,

    [ItemName("Faerie Scarf"), ItemDescription("A scarf with a mystical air about it."), ItemCategory(ItemCategory.Scarves)]
    FaerieScarf,

    [ItemName("Flame Scarf"), ItemDescription("A scarf that resembles fire."), ItemCategory(ItemCategory.Scarves)]
    Firemuffler,

    [ItemName("Mystical Scarf"), ItemDescription("The sacred scarf of a robed figure."), ItemCategory(ItemCategory.Scarves)]
    JourneyScarf,

    [ItemIdAttribute("Made-to-order"), ItemName("Order-made Scarf"), ItemDescription("A personalized scarf that can be dyed the color of your choice."), ItemCategory(ItemCategory.Scarves)]
    Madetoorder,

    [ItemName("Scarf"), ItemDescription("Miriam's favorite scarf."), ItemCategory(ItemCategory.Scarves)]
    Muffler,

    [ItemName("Gunman's Scarf"), ItemDescription("A scarf that makes you quick on the draw and  increases firing rate."), ItemCategory(ItemCategory.Scarves)]
    Outlinestyle,

    [ItemName("Over the Rainbow"), ItemDescription("A scarf that brings good luck and blessings to whoever finds it."), ItemCategory(ItemCategory.Scarves)]
    OvertheRainbow,

    [ItemName("Fangshi Scarf"), ItemDescription("A scarf woven from the threads of a rare insect."), ItemCategory(ItemCategory.Scarves)]
    Sendomuffler,

    [ItemName("Talisman Scarf"), ItemDescription("A scarf made of many individually prepared talismans."), ItemCategory(ItemCategory.Scarves)]
    TalismanScarf,

    [ItemName("Tattered Scarf"), ItemDescription("A scarf made of ragged cloth."), ItemCategory(ItemCategory.Scarves)]
    Turbinemuffler,

    [ItemName("Bunny Scarf"), ItemDescription("A scarf with a rabbit motif that is certain to get you hopping."), ItemCategory(ItemCategory.Scarves)]
    Usagimuffler,

    [ItemName("Vampiric Choker"), ItemDescription("Naughty neckwear for a nubile nape."), ItemCategory(ItemCategory.Scarves)]
    VampiricChoker,

    [ItemName("Adversity Ring"), ItemDescription("A ring that causes status inflictions to increase your power."), ItemCategory(ItemCategory.Accessory)]
    Adversityring,

    [ItemName("A Kinda Funny Mask"), ItemDescription("Could be funnier. Could be less."), ItemCategory(ItemCategory.Accessory)]
    AKindaFunnyMask,

    [ItemName("Assassin's Ring"), ItemDescription("A ring that slightly increases damage and frequency of critical hits."), ItemCategory(ItemCategory.Accessory)]
    AssassinsRing,

    [ItemName("Big Mustache"), ItemDescription("Bold bristles that evoke an AWESOME sense of daditude!"), ItemCategory(ItemCategory.Accessory)]
    BigMustache,

    [ItemName("Black Belt"), ItemDescription("A belt worn by Eastern masters of the martial arts. Increases attack speed."), ItemCategory(ItemCategory.Accessory)]
    Blackbelt,

    [ItemName("Thick Glasses"), ItemDescription("Eyeglasses with thick lenses."), ItemCategory(ItemCategory.Accessory)]
    Bottleglasses,

    [ItemName("Hyperventilator"), ItemDescription("A mask designed to push the wearer to his or her physical limit."), ItemCategory(ItemCategory.Accessory)]
    Breathingforcedmask,

    [ItemName("Critical Ring"), ItemDescription("A ring that increases the likelihood of critical hits."), ItemCategory(ItemCategory.Accessory)]
    Criticalring,

    [ItemName("Crow Mask"), ItemDescription("A mask designed to resemble a crow."), ItemCategory(ItemCategory.Accessory)]
    Crowmask,

    [ItemName("Cursed Ring"), ItemDescription("A ring that embodies all the hatred in this world."), ItemCategory(ItemCategory.Accessory)]
    Cursering,

    [ItemName("Cutpurse's Ring"), ItemDescription("A ring that makes enemies more likely to drop gold."), ItemCategory(ItemCategory.Accessory)]
    CutpursesRing,

    [ItemName("Dance Mask"), ItemDescription("A mask used at dances."), ItemCategory(ItemCategory.Accessory)]
    DanceMask,

    [ItemName("Demon Necklace"), ItemDescription("A demon necklace that eliminates vulnerability when using shards. "), ItemCategory(ItemCategory.Accessory)]
    DemonNecklace,

    [ItemName("Elf Ears"), ItemDescription("Wearable ears that resemble those of a certain sylvan race."), ItemCategory(ItemCategory.Accessory)]
    Elfearcap,

    [ItemName("Convex Glasses"), ItemDescription("Eyeglasses to correct farsightedness. Everything looks bigger!"), ItemCategory(ItemCategory.Accessory)]
    Enlargedglasses,

    [ItemName("Eyeglasses"), ItemDescription("Eyeglasses with plain lenses."), ItemCategory(ItemCategory.Accessory)]
    Eyeglasses,

    [ItemName("Usagi Mask"), ItemDescription("This delicate, hand-crafted mask is sure to attract good favor from yokai and kami alike."), ItemCategory(ItemCategory.Accessory)]
    FestivalMask,

    [ItemName("Flame Ring"), ItemDescription("A ring blessed by a fire god. Strengthens fire abilities."), ItemCategory(ItemCategory.Accessory)]
    Flamering,

    [ItemName("Gambler's Ring"), ItemDescription("A ring that bestows both luck and pluck to would-be gamblers."), ItemCategory(ItemCategory.Accessory)]
    Gamblersring,

    [ItemName("Gebel's Glasses"), ItemDescription("Hand-me-downs from Johannes. A reminder of days that are now lost."), ItemCategory(ItemCategory.Accessory)]
    Gebelsglasses,

    [ItemName("Gold Power Ring"), ItemDescription("A ring that increases attack power in proportion to your wealth."), ItemCategory(ItemCategory.Accessory)]
    Goldmanring,

    [ItemId("HeyI’mGrump"), ItemName("Hey I'm Grump"), ItemDescription("Arin's head from Game Grumps. Useless."), ItemCategory(ItemCategory.Accessory), SerializedAsUnicode]
    HeyImGrump,

    [ItemName("Eye of Horus"), ItemDescription("A monocle that lets you look down on the world like a bird."), ItemCategory(ItemCategory.Accessory)]
    Horuseyes,

    [ItemId("I’mNotSoGrump"), ItemName("I'm Not So Grump"), ItemDescription("Dan's head from Game Grumps. Pointless"), ItemCategory(ItemCategory.Accessory), SerializedAsUnicode]
    ImNotSoGrump,

    [ItemName("Ice Ring"), ItemDescription("A ring blessed by an ice god. Strengthens ice abilities."), ItemCategory(ItemCategory.Accessory)]
    Icering,

    [ItemName("Lethality Ring"), ItemDescription("A ring that increases the damage you inflict with critical hits."), ItemCategory(ItemCategory.Accessory)]
    LethalityRing,

    [ItemName("Communicator"), ItemDescription("This headset helps Miri boldly proclaim her dedication to punish darkness in all its forms."), ItemCategory(ItemCategory.Accessory)]
    MagicalGirlAccessory,

    [ItemName("Gauge Glasses"), ItemDescription("Magical eyeglasses that reveal enemy health."), ItemCategory(ItemCategory.Accessory)]
    Measurementglasses,

    [ItemName("Moon Belt"), ItemDescription("A belt with a moon design that improves your backstep ability."), ItemCategory(ItemCategory.Accessory)]
    Moonring,

    [ItemName("Necklace"), ItemDescription("A simple necklace that matches most outfits."), ItemCategory(ItemCategory.Accessory)]
    Necklace,

    [ItemName("Nose Glasses"), ItemDescription("Eyeglasses with a false nose attached to the frame."), ItemCategory(ItemCategory.Accessory)]
    NoseEyeglasses,

    [ItemName("Ofuda Talisman"), ItemDescription("A ritual Eastern talisman imbued with the protection of the kami."), ItemCategory(ItemCategory.Accessory)]
    OfudaTalisman,

    [ItemName("Plague Doctor Face"), ItemDescription("A grim bird for swimming in death."), ItemCategory(ItemCategory.Accessory)]
    PlagueDoctorFace,

    [ItemName("Plunderer's Ring"), ItemDescription("A ring that increases the rate of item drops."), ItemCategory(ItemCategory.Accessory)]
    PlunderersRing,

    [ItemName("Ring"), ItemDescription("A cheap, common ring."), ItemCategory(ItemCategory.Accessory)]
    Ring,

    [ItemName("Solomon's Ring"), ItemDescription("A ring that increases how often enemies yield shards."), ItemCategory(ItemCategory.Accessory)]
    RingofSolomon,

    [ItemName("Risk Ring"), ItemDescription("A ring that sacrifices defense to increase attack power."), ItemCategory(ItemCategory.Accessory)]
    Riskring,

    [ItemName("Rose Ring"), ItemDescription("A ring that increases the amount of magic restored by mana roses."), ItemCategory(ItemCategory.Accessory)]
    RoseRing,

    [ItemName("Rusted Ring"), ItemDescription("A rusted ring."), ItemCategory(ItemCategory.Accessory)]
    RustedRing,

    [ItemName("Safe Ring"), ItemDescription("A ring that sacrifices attack power to increase defense."), ItemCategory(ItemCategory.Accessory)]
    Safering,

    [ItemName("Sarashi"), ItemDescription("A simple garment worn for support."), ItemCategory(ItemCategory.Accessory)]
    Sarashi,

    [ItemName("Half-Genie Tiara"), ItemDescription("The perfect accessory for lethal-looking locks."), ItemCategory(ItemCategory.Accessory)]
    ShantaeTiara,

    [ItemName("Half-Genie Vest"), ItemDescription("Short on length, long on fashion."), ItemCategory(ItemCategory.Accessory)]
    ShantaeVest,

    [ItemName("Silver Power Ring"), ItemDescription("A ring that increases attack power in proportion to the number of enemies you have slain."), ItemCategory(ItemCategory.Accessory)]
    Silvermanring,

    [ItemName("Skull Necklace"), ItemDescription("A necklace that amplifies the power of shards at increased magic cost. "), ItemCategory(ItemCategory.Accessory)]
    SkullNecklace,

    [ItemName("Speed Belt"), ItemDescription("A strange belt that compels wearers to run.  Increases movement speed."), ItemCategory(ItemCategory.Accessory)]
    Speedstar,

    [ItemName("Stone Mask"), ItemDescription("A stone mask once used by a tribal chief during  ritual sacrifices."), ItemCategory(ItemCategory.Accessory)]
    Stonemask,

    [ItemName("Strider Belt"), ItemDescription("A belt made to withstand rough movement. Improves sliding ability."), ItemCategory(ItemCategory.Accessory)]
    Striderbelt,

    [ItemName("Sunglasses"), ItemDescription("Eyeglasses designed to protect the eyes from strong sunlight."), ItemCategory(ItemCategory.Accessory)]
    sunglasses,

    [ItemId("The-BazMask"), ItemName("The-Baz Mask"), ItemDescription("A cheap mask that prevents the wearer from finding success."), ItemCategory(ItemCategory.Accessory)]
    TheBazMask,

    [ItemName("Thunder Ring"), ItemDescription("A ring blessed by a thunder god. Strengthens thunder abilities."), ItemCategory(ItemCategory.Accessory)]
    Thunderring,

    [ItemName("Voice Changer"), ItemDescription("A mask devised by a playful inventor. It distorts the wearer's voice."), ItemCategory(ItemCategory.Accessory)]
    Transformationmask,

    [ItemName("Unicorn Ring"), ItemDescription("A ring that amplifies the restorative properties of items you use."), ItemCategory(ItemCategory.Accessory)]
    UnicornRing,

    [ItemName("Vampiric Wings"), ItemDescription("Purely decorative wings. Pair with an impure decolletage."), ItemCategory(ItemCategory.Accessory)]
    VampiricWings,

    [ItemName("Traverser's Ring"), ItemDescription("A ring that increases attack power in proportion to how much of the map you have completed."), ItemCategory(ItemCategory.Accessory)]
    Walkerking,

    [ItemName("Warlock's Necklace"), ItemDescription("The beloved necklace of a great sorcerer. Reduces magic depletion."), ItemCategory(ItemCategory.Accessory)]
    WarlocksNecklace,

    [ItemName("Weighted Ring"), ItemDescription("A heavy ring that increases your experience intake."), ItemCategory(ItemCategory.Accessory)]
    WeightedRing,

    [ItemName("Shinobi Garb"), ItemDescription("A garment from the Far East that allows you to move silently."), ItemCategory(ItemCategory.BodyArmor)]
    Ashinbacostume,

    [ItemName("Assassin's Vest"), ItemDescription("Assassin's garb that increases the speed of your attacks."), ItemCategory(ItemCategory.BodyArmor)]
    Assassinvest,

    [ItemName("Hound Vest"), ItemDescription("A vest crafted from the pelt of a ferocious beast."), ItemCategory(ItemCategory.BodyArmor)]
    Beastleather,

    [ItemName("Aegis Plate"), ItemDescription("A breastplate that nullifies all damage from traps and hazardous environments."), ItemCategory(ItemCategory.BodyArmor)]
    BreastplateofAguilar,

    [ItemName("Brigandine"), ItemDescription("Armor fortified by riveting a line of metal plates to  the inside. "), ItemCategory(ItemCategory.BodyArmor)]
    Brigandine,

    [ItemName("Kalasiris"), ItemDescription("A see-through tunic worn by affluent members of ancient Egyptian society."), ItemCategory(ItemCategory.BodyArmor)]
    Caracilis,

    [ItemName("Chainmail"), ItemDescription("Armor that is made of many small metal rings linked together."), ItemCategory(ItemCategory.BodyArmor)]
    Chainmail,

    [ItemName("Chemise"), ItemDescription("A thin dress made of muslin."), ItemCategory(ItemCategory.BodyArmor)]
    Chemisedress,

    [ItemName("Mantua (Aurora)"), ItemDescription("A loose and elegant gown."), ItemCategory(ItemCategory.BodyArmor)]
    COL_Au15_Dress,

    [ItemName("Coronation Gown (Aurora)"), ItemDescription("A breathtaking dress fit for no less than a queen."), ItemCategory(ItemCategory.BodyArmor)]
    COL_Au20_Dress,

    [ItemName("Tea Dress (Aurora)"), ItemDescription("A light dress for tea parties."), ItemCategory(ItemCategory.BodyArmor)]
    COL_Au5_Dress,

    [ItemName("Bronze Chestguard"), ItemDescription("A chestguard made of bronze."), ItemCategory(ItemCategory.BodyArmor)]
    Copperbreastplate,

    [ItemName("Coronation Gown"), ItemDescription("A breathtaking dress fit for no less than a queen."), ItemCategory(ItemCategory.BodyArmor)]
    Coronationdress,

    [ItemName("Country Dress"), ItemDescription("A common, plain dress."), ItemCategory(ItemCategory.BodyArmor)]
    Countrydress,

    [ItemName("Scarlet Dress"), ItemDescription("A blood red dress designed to catch the eye."), ItemCategory(ItemCategory.BodyArmor)]
    Crimsondress,

    [ItemName("Crusader's Armor"), ItemDescription("Armor emblazoned with a cross."), ItemCategory(ItemCategory.BodyArmor)]
    CrusadeArmor,

    [ItemName("Crystal Armor"), ItemDescription("Specially crafted armor with crystal scales. Resistant to darkness."), ItemCategory(ItemCategory.BodyArmor)]
    Crystalscale,

    [ItemName("Knight's Cuirass"), ItemDescription("A knight's durable chest armor."), ItemCategory(ItemCategory.BodyArmor)]
    Cureus,

    [ItemName("Dragon Armor"), ItemDescription("Armor fashioned from a weave of dragon scales."), ItemCategory(ItemCategory.BodyArmor)]
    DragonScale,

    [ItemName("Duelist's Guard"), ItemDescription("A chestguard worn by duelists."), ItemCategory(ItemCategory.BodyArmor)]
    DuelBrest,

    [ItemName("Feather Robe"), ItemDescription("A robe so feather-light that a single hit may send you fluttering away."), ItemCategory(ItemCategory.BodyArmor)]
    Featherrobe,

    [ItemName("Japanesque Robe"), ItemDescription("Whether hanging at Hanami or mingling at Matsuri, this high quality robe makes the whole week seem golden."), ItemCategory(ItemCategory.BodyArmor)]
    FestivalKimono,

    [ItemName("Japanesque Robe (Level 2)"), ItemDescription("Whether hanging at Hanami or mingling at Matsuri, this high quality robe makes the whole week seem golden."), ItemCategory(ItemCategory.BodyArmor)]
    FestivalKimono1,

    [ItemName("Japanesque Robe (Level 3)"), ItemDescription("Whether hanging at Hanami or mingling at Matsuri, this high quality robe makes the whole week seem golden."), ItemCategory(ItemCategory.BodyArmor)]
    FestivalKimono2,

    [ItemName("Japanesque Robe (Level 4)"), ItemDescription("Whether hanging at Hanami or mingling at Matsuri, this high quality robe makes the whole week seem golden."), ItemCategory(ItemCategory.BodyArmor)]
    FestivalKimono3,

    [ItemName("Japanesque Robe (Level 5)"), ItemDescription("Whether hanging at Hanami or mingling at Matsuri, this high quality robe makes the whole week seem golden."), ItemCategory(ItemCategory.BodyArmor)]
    FestivalKimono4,

    [ItemName("Japanesque Robe (Level 6)"), ItemDescription("Whether hanging at Hanami or mingling at Matsuri, this high quality robe makes the whole week seem golden."), ItemCategory(ItemCategory.BodyArmor)]
    FestivalKimono5,

    [ItemName("Majestic Plate"), ItemDescription("A breastplate presented by a ruler to one who performs great deeds."), ItemCategory(ItemCategory.BodyArmor)]
    GloriousBreast,

    [ItemName("Gold Breastplate"), ItemDescription("A gold breastplate so bright that it dazzles beholders."), ItemCategory(ItemCategory.BodyArmor)]
    Goldbreastplate,

    [ItemName("Imperial Armor"), ItemDescription("Luxurious and stately armor that bears nary a scratch."), ItemCategory(ItemCategory.BodyArmor)]
    ImperialArmor,

    [ItemName("Iron Breastplate"), ItemDescription("An iron breastplate designed to not impede movement."), ItemCategory(ItemCategory.BodyArmor)]
    Ironbreastplate,

    [ItemName("Apron"), ItemDescription("An apron imbued with the soul of a departed chef."), ItemCategory(ItemCategory.BodyArmor)]
    IronMansApron,

    [ItemName("Kung Fu Vest"), ItemDescription("A traditional Eastern garment designed for ease of movement."), ItemCategory(ItemCategory.BodyArmor)]
    KungFuBest,

    [ItemName("Lamellar Armor"), ItemDescription("Armor made of metal plates arranged in rows."), ItemCategory(ItemCategory.BodyArmor)]
    LamellarArmor,

    [ItemName("Miri Dress"), ItemDescription("Loli fashion for pretty sailors protecting truth and love, amidst sparkles and rainbows."), ItemCategory(ItemCategory.BodyArmor)]
    MagicalGirlBody,

    [ItemName("Miri Dress (Level 2)"), ItemDescription("Loli fashion for pretty sailors protecting truth and love, amidst sparkles and rainbows."), ItemCategory(ItemCategory.BodyArmor)]
    MagicalGirlBody1,

    [ItemName("Miri Dress (Level 3)"), ItemDescription("Loli fashion for pretty sailors protecting truth and love, amidst sparkles and rainbows."), ItemCategory(ItemCategory.BodyArmor)]
    MagicalGirlBody2,

    [ItemName("Miri Dress (Level 4)"), ItemDescription("Loli fashion for pretty sailors protecting truth and love, amidst sparkles and rainbows."), ItemCategory(ItemCategory.BodyArmor)]
    MagicalGirlBody3,

    [ItemName("Miri Dress (Level 5)"), ItemDescription("Loli fashion for pretty sailors protecting truth and love, amidst sparkles and rainbows."), ItemCategory(ItemCategory.BodyArmor)]
    MagicalGirlBody4,

    [ItemName("Miri Dress (Level 6)"), ItemDescription("Loli fashion for pretty sailors protecting truth and love, amidst sparkles and rainbows."), ItemCategory(ItemCategory.BodyArmor)]
    MagicalGirlBody5,

    [ItemName("Mantua"), ItemDescription("A loose and elegant gown."), ItemCategory(ItemCategory.BodyArmor)]
    Maturedress,

    [ItemName("Riding Habit"), ItemDescription("A riding garment designed with elasticity in mind."), ItemCategory(ItemCategory.BodyArmor)]
    RidingHabit,

    [ItemName("Scale Armor"), ItemDescription("Armor made by weaving together metal plates in a  scale pattern."), ItemCategory(ItemCategory.BodyArmor)]
    Scalearmor,

    [ItemName("Half-Genie Outfit"), ItemDescription("The go-to garb for Guardian Genies."), ItemCategory(ItemCategory.BodyArmor)]
    ShantaeOutfit1,

    [ItemName("Half-Genie Outfit (Level 2)"), ItemDescription("The go-to garb for Guardian Genies."), ItemCategory(ItemCategory.BodyArmor)]
    ShantaeOutfit2,

    [ItemName("Half-Genie Outfit (Level 3)"), ItemDescription("The go-to garb for Guardian Genies."), ItemCategory(ItemCategory.BodyArmor)]
    ShantaeOutfit3,

    [ItemName("Half-Genie Outfit (Level 4)"), ItemDescription("The go-to garb for Guardian Genies."), ItemCategory(ItemCategory.BodyArmor)]
    ShantaeOutfit4,

    [ItemName("Half-Genie Outfit (Level 5)"), ItemDescription("The go-to garb for Guardian Genies."), ItemCategory(ItemCategory.BodyArmor)]
    ShantaeOutfit5,

    [ItemName("Half-Genie Outfit (Level 6)"), ItemDescription("The go-to garb for Guardian Genies."), ItemCategory(ItemCategory.BodyArmor)]
    ShantaeOutfit6,

    [ItemName("Tatenashi"), ItemDescription("Armor so tough that its name claims a shield is unnecessary."), ItemCategory(ItemCategory.BodyArmor)]
    Shieldless,

    [ItemName("Flame Mail"), ItemDescription("Armor mantled in searing fire."), ItemCategory(ItemCategory.BodyArmor)]
    ShiningBreast,

    [ItemName("Ex Shovel Armor"), ItemDescription("Armor formerly worn by a certain valiant knight."), ItemCategory(ItemCategory.BodyArmor)]
    Shovelarmorsarmor,

    [ItemName("Silk Dress"), ItemDescription("A light dress with a beautiful white sheen."), ItemCategory(ItemCategory.BodyArmor)]
    Silkdress,

    [ItemName("Silver Breastplate"), ItemDescription("A silver breastplate imbued with protections against evil."), ItemCategory(ItemCategory.BodyArmor)]
    Silverbreastplate,

    [ItemName("Leather Chestguard"), ItemDescription("A chestguard made of tanned leather."), ItemCategory(ItemCategory.BodyArmor)]
    Skinbreastplate,

    [ItemName("Spiked Breastplate"), ItemDescription("A breastplate lined with sharp spikes that injure whoever touches them."), ItemCategory(ItemCategory.BodyArmor)]
    SpikeBreast,

    [ItemName("Steel Breastplate"), ItemDescription("A fortified steel breastplate."), ItemCategory(ItemCategory.BodyArmor)]
    Steelbreastplate,

    [ItemName("Tea Dress"), ItemDescription("A light dress for tea parties."), ItemCategory(ItemCategory.BodyArmor)]
    Teadress,

    [ItemName("Tunic"), ItemDescription("A long shirt made of plain cloth."), ItemCategory(ItemCategory.BodyArmor)]
    Tunic,

    [ItemName("Valkyrie Dress"), ItemDescription("A warmaiden's distinct dress."), ItemCategory(ItemCategory.BodyArmor)]
    Valkyriedress,

    [ItemName("Vampiric Skinsuit"), ItemDescription("Armor akin to wearing nothing at all."), ItemCategory(ItemCategory.BodyArmor)]
    VampiricSkinsuit,

    [ItemName("Vampiric Skinsuit (Level 2)"), ItemDescription("Armor akin to wearing nothing at all."), ItemCategory(ItemCategory.BodyArmor)]
    VampiricSkinsuit1,

    [ItemName("Vampiric Skinsuit (Level 3)"), ItemDescription("Armor akin to wearing nothing at all."), ItemCategory(ItemCategory.BodyArmor)]
    VampiricSkinsuit2,

    [ItemName("Vampiric Skinsuit (Level 4)"), ItemDescription("Armor akin to wearing nothing at all."), ItemCategory(ItemCategory.BodyArmor)]
    VampiricSkinsuit3,

    [ItemName("Vampiric Skinsuit (Level 5)"), ItemDescription("Armor akin to wearing nothing at all."), ItemCategory(ItemCategory.BodyArmor)]
    VampiricSkinsuit4,

    [ItemName("Vampiric Skinsuit (Level 6)"), ItemDescription("Armor akin to wearing nothing at all."), ItemCategory(ItemCategory.BodyArmor)]
    VampiricSkinsuit5,

    [ItemName("Polonaise"), ItemDescription("Loungewear that is all the rage with fashionable noble ladies."), ItemCategory(ItemCategory.BodyArmor)]
    VoronezDress,

    [ItemName("Ambrosia"), ItemDescription("A legendary fruit said to grant immortality."), ItemCategory(ItemCategory.Ingredients)]
    Ambrosia,

    [ItemName("Apple"), ItemDescription("A juicy, red fruit that can be eaten without removing the skin."), ItemCategory(ItemCategory.Ingredients)]
    Apple,

    [ItemName("Baking Soda"), ItemDescription("Powder used to make dough rise or remove stains from teacups."), ItemCategory(ItemCategory.Ingredients)]
    BakingSoda,

    [ItemName("Bread"), ItemDescription("A flour-based dough that has been leavened and then baked."), ItemCategory(ItemCategory.Ingredients)]
    Bread,

    [ItemName("Butter"), ItemDescription("A dairy product used in everything from sweets to  sautés."), ItemCategory(ItemCategory.Ingredients)]
    Butter,

    [ItemName("Cacao Bean"), ItemDescription("A fragrant bean that tastes bitter when you bite into it."), ItemCategory(ItemCategory.Ingredients)]
    CacaoBean,

    [ItemName("Fell Leaf"), ItemDescription("The nutrient-rich leaf of a Cerbera. It is valued for its medicinal properties."), ItemCategory(ItemCategory.Ingredients)]
    CerberaLeaf,

    [ItemName("Cheese"), ItemDescription("A dairy product made by fermenting milk."), ItemCategory(ItemCategory.Ingredients)]
    Cheese,

    [ItemName("Chinese Noodles"), ItemDescription("Thin, frizzly noodles from China that are often consumed with broth."), ItemCategory(ItemCategory.Ingredients)]
    ChineseNoodles,

    [ItemName("Cinnamon"), ItemDescription("A spice with a unique aroma made from a type of tree bark."), ItemCategory(ItemCategory.Ingredients)]
    Cinnamon,

    [ItemName("Clam"), ItemDescription("A shellfish that needs to be purged of sand before eating."), ItemCategory(ItemCategory.Ingredients)]
    Clam,

    [ItemName("Cocoa"), ItemDescription("A sweet beverage made from cacao beans."), ItemCategory(ItemCategory.Ingredients)]
    Cocoa,

    [ItemName("Consommé"), ItemDescription("A clear, amber broth. Delicious served both hot and cold."), ItemCategory(ItemCategory.Ingredients), SerializedAsUnicode]
    Consommé,

    [ItemName("Corn"), ItemDescription("A vegetable covered in small yellow kernels. Feeds people and livestock."), ItemCategory(ItemCategory.Ingredients)]
    Corn,

    [ItemName("Crepe Dough"), ItemDescription("A dough made by soaking flour in water. It is rolled paper thin."), ItemCategory(ItemCategory.Ingredients)]
    CrepeDough,

    [ItemName("Curry Powder"), ItemDescription("A powdered mixture of fragrant spices that whets the appetite."), ItemCategory(ItemCategory.Ingredients)]
    CurryPowder,

    [ItemName("Curry Sauce"), ItemDescription("A hot sauce made from a mixture of fragrant spices."), ItemCategory(ItemCategory.Ingredients)]
    CurrySauce,

    [ItemName("Fey Leaf"), ItemDescription("The poisonous leaf of a Datura. It can be made edible if properly heated. "), ItemCategory(ItemCategory.Ingredients)]
    DaturaLeaf,

    [ItemName("Aquatic Filet"), ItemDescription("Deeseama meat. It's extremely tough and rubbery."), ItemCategory(ItemCategory.Ingredients)]
    DeeseamaFilet,

    [ItemName("Dragon Egg"), ItemDescription("An egg laid by a dragon. Fetches good coin among gourmands."), ItemCategory(ItemCategory.Ingredients)]
    DragonsEgg,

    [ItemName("Egg"), ItemDescription("A freshly laid chicken egg. You can pick up the yolk  with your fingers."), ItemCategory(ItemCategory.Ingredients)]
    Egg,

    [ItemName("Flour"), ItemDescription("A grain found in nearly every household and used in  many dishes."), ItemCategory(ItemCategory.Ingredients)]
    Flour,

    [ItemName("Forneus Filet"), ItemDescription("Meat from Forneus. It's classified under white fish."), ItemCategory(ItemCategory.Ingredients)]
    ForneusFilet,

    [ItemId("G-LabolasFilet"), ItemName("G-Bone Steak"), ItemDescription("Meat from a glasya-labolas. The light flavor compliments any cuisine."), ItemCategory(ItemCategory.Ingredients)]
    GLabolasFilet,

    [ItemName("Garlic"), ItemDescription("A spice with a distinct smell that vampires utterly detest."), ItemCategory(ItemCategory.Ingredients)]
    Garlic,

    [ItemName("Ginger"), ItemDescription("A spice that imparts a refreshing bite and warms  the body."), ItemCategory(ItemCategory.Ingredients)]
    Ginger,

    [ItemName("Flying Beef"), ItemDescription("Scrumptious haagenti meat that will have you mooing for more."), ItemCategory(ItemCategory.Ingredients)]
    HaagentiFilet,

    [ItemName("Heavy Cream"), ItemDescription("A thick, white liquid made from butterfat."), ItemCategory(ItemCategory.Ingredients)]
    HeavyCream,

    [ItemName("Lemon"), ItemDescription("A yellow and very sour fruit that can be used to  mask the smell of fish."), ItemCategory(ItemCategory.Ingredients)]
    Lemon,

    [ItemName("Milk"), ItemDescription("Fresh milk packed with nutrients to help you grow strong."), ItemCategory(ItemCategory.Ingredients)]
    Milk,

    [ItemName("Beast Milk"), ItemDescription("Milk taken from a zagan. Truly the pinnacle of dairy delights."), ItemCategory(ItemCategory.Ingredients)]
    MilkofZagan,

    [ItemName("Miso"), ItemDescription("A paste of fermented soybeans that's hard on the eyes but full of flavor."), ItemCategory(ItemCategory.Ingredients)]
    Miso,

    [ItemName("Miso Broth"), ItemDescription("A hearty broth made with dissolved miso."), ItemCategory(ItemCategory.Ingredients)]
    MisoBroth,

    [ItemName("Moco Oil"), ItemDescription("Oil harvested from moco weeds. Works wonders on the skin."), ItemCategory(ItemCategory.Ingredients)]
    MocoOil,

    [ItemName("Moco Leek"), ItemDescription("An onion harvested from moco weeds. Cooking it brings out its sweetness."), ItemCategory(ItemCategory.Ingredients)]
    MocoOnion,

    [ItemName("Rice"), ItemDescription("A grain that features prominently in Eastern diets."), ItemCategory(ItemCategory.Ingredients)]
    Paddy,

    [ItemName("Pasta"), ItemDescription("Noodles made from durum wheat flour. They become chewy once boiled."), ItemCategory(ItemCategory.Ingredients)]
    Pasta,

    [ItemName("Black Pepper"), ItemDescription("A spice used to punch up cooking. Often appears next to salt."), ItemCategory(ItemCategory.Ingredients)]
    Pepper,

    [ItemName("Pie Dough"), ItemDescription("A dough made from flour and plenty of butter.  Makes a nice, flaky crust."), ItemCategory(ItemCategory.Ingredients)]
    PieDough,

    [ItemName("Pizza Dough"), ItemDescription("A flour-based dough that is kneaded and stretched into a thin circle."), ItemCategory(ItemCategory.Ingredients)]
    PizzaDough,

    [ItemName("Potato"), ItemDescription("A hardy tuber that thrives in poor soil. Beware the poisonous sprouts."), ItemCategory(ItemCategory.Ingredients)]
    Potato,

    [ItemName("Red Bean"), ItemDescription("A type of bean grown in the Orient and used to make desserts."), ItemCategory(ItemCategory.Ingredients)]
    RedBean,

    [ItemName("Red Bean Paste"), ItemDescription("Boiled, sweetened red beans that may be mashed and skinned or left whole."), ItemCategory(ItemCategory.Ingredients)]
    RedBeanPaste,

    [ItemName("Rennet"), ItemDescription("A complex of enzymes used to produce cheese."), ItemCategory(ItemCategory.Ingredients)]
    Rennet,

    [ItemName("Salt Broth"), ItemDescription("A simple broth that has been flavored with salt."), ItemCategory(ItemCategory.Ingredients)]
    SaltBroth,

    [ItemName("Soda Water"), ItemDescription("Carbonated water. Mix it with fruit juice for a  delicious beverage."), ItemCategory(ItemCategory.Ingredients)]
    Soda,

    [ItemName("Soy Broth"), ItemDescription("A Nipponese broth flavored with soy sauce."), ItemCategory(ItemCategory.Ingredients)]
    SoyBroth,

    [ItemName("Soy Sauce"), ItemDescription("A liquid condiment made from soybeans that is  essential to Eastern cuisine."), ItemCategory(ItemCategory.Ingredients)]
    SoySauce,

    [ItemName("Strawberry"), ItemDescription("A sweet and sour fruit that makes delicious preserves."), ItemCategory(ItemCategory.Ingredients)]
    Strawberry,

    [ItemName("Sugar"), ItemDescription("A sweetener and essential ingredient when making  desserts."), ItemCategory(ItemCategory.Ingredients)]
    Sugar,

    [ItemName("Tomato"), ItemDescription("A ruby-red vegetable that appears in everything from salads to stews."), ItemCategory(ItemCategory.Ingredients)]
    Tomato,

    [ItemName("Tonkotsu Broth"), ItemDescription("A cloudy broth made by slowly boiling pork bones."), ItemCategory(ItemCategory.Ingredients)]
    TonkotsuBroth,

    [ItemName("Sea Urchin"), ItemDescription("A sea delicacy best enjoyed raw— the fresher the better."), ItemCategory(ItemCategory.Ingredients)]
    Uni,

    [ItemName("Vongole Sauce"), ItemDescription("A clam-based sauce that tastes delicious when tossed with pasta."), ItemCategory(ItemCategory.Ingredients)]
    VongoleSauce,

    [ItemName("White Sauce"), ItemDescription("A white sauce made from a roux of fat and flour."), ItemCategory(ItemCategory.Ingredients)]
    WhiteSauce,

    [ItemName("Plume Pork"), ItemDescription("Pork from a plume parma. Try the sirloin; it's delicious."), ItemCategory(ItemCategory.Ingredients)]
    YorktonMeat,

    [ItemName("Beast Beef"), ItemDescription("The meat of a zagan. It melts in your mouth with each bite."), ItemCategory(ItemCategory.Ingredients)]
    ZaganMeat,

    [ItemName("Apple Juice"), ItemDescription("Apple juice made the right way: by carefully selecting the best fruit."), ItemCategory(ItemCategory.Food)]
    AppleJuice,

    [ItemName("Apple Pie"), ItemDescription("A pie with an apple slice filling that gives it a subtle sweetness."), ItemCategory(ItemCategory.Food)]
    ApplePie,

    [ItemName("Apple Risotto"), ItemDescription("An improvised risotto made with apple and rice. Surprisingly good."), ItemCategory(ItemCategory.Food)]
    AppleRisotto,

    [ItemName("Beef Curry"), ItemDescription("An extravagant curry and rice plate loaded with beef. Seconds, please!"), ItemCategory(ItemCategory.Food)]
    BeefCurry,

    [ItemName("Berry Spaghetti"), ItemDescription("An improvised dessert pasta made with strawberries and cream. It works!"), ItemCategory(ItemCategory.Food)]
    BerrySpaghetti,

    [ItemName("Cheesecake"), ItemDescription("A moist and mildly sweet cake made with an ample helping of cheese."), ItemCategory(ItemCategory.Food)]
    CheeseCake,

    [ItemName("Chicken Casserole"), ItemDescription("Chicken and rice covered in white sauce and baked. Don't burn yourself."), ItemCategory(ItemCategory.Food)]
    ChickenCasserole,

    [ItemName("Chicken Curry"), ItemDescription("Curry and rice made with chicken that has soaked up the flavor."), ItemCategory(ItemCategory.Food)]
    ChickenCurry,

    [ItemName("Chicken Sauté"), ItemDescription("A chicken thigh fried in fat, yielding crispy skin and juicy meat."), ItemCategory(ItemCategory.Food), SerializedAsUnicode]
    ChickenSauté,

    [ItemName("Chiffon Cake"), ItemDescription("A silky sponge cake that's nothing short of a culinary miracle."), ItemCategory(ItemCategory.Food)]
    ChiffonCake,

    [ItemName("Chocolate Cake"), ItemDescription("A luxurious chocolate cake. Also, a cavity waiting to happen."), ItemCategory(ItemCategory.Food)]
    ChocolateCake,

    [ItemName("Chocolate Cookies"), ItemDescription("A cookie made with bitter chocolate for a more sophisticated flavor."), ItemCategory(ItemCategory.Food)]
    ChocolateCookies,

    [ItemName("Chocolate Crepe"), ItemDescription("A simple crepe that gets a subtle bitter note from the chocolate."), ItemCategory(ItemCategory.Food)]
    ChocolateCrepe,

    [ItemName("Cinnamon Cookies"), ItemDescription("A fragrant cookie that pairs well with black tea."), ItemCategory(ItemCategory.Food)]
    CinnamonCookies,

    [ItemName("Classic Spaghetti"), ItemDescription("Pasta tossed in tomato sauce. Certain to put a smile on any face."), ItemCategory(ItemCategory.Food)]
    ClassicSpaghetti,

    [ItemName("Cookies"), ItemDescription("A classic baked sweet that can be made in all shapes and sizes."), ItemCategory(ItemCategory.Food)]
    Cookies,

    [ItemName("Corn Chowder"), ItemDescription("A thick but smooth corn soup that hovers between a food and a drink."), ItemCategory(ItemCategory.Food)]
    CornChowder,

    [ItemId("Curry&Rice"), ItemName("Curry & Rice"), ItemDescription("Curry sauce over rice. Delicious, but imagine it with meat or vegetables..."), ItemCategory(ItemCategory.Food)]
    CurryAndRice,

    [ItemName("Dark Matter"), ItemDescription("An ominous black mass that is probably edible...but you first."), ItemCategory(ItemCategory.Food)]
    DarkMatter,

    [ItemName("Egg on Rice"), ItemDescription("The ultimate everyman's breakfast: raw egg and soy sauce over rice."), ItemCategory(ItemCategory.Food)]
    EggonRice,

    [ItemName("Egg Soufflé"), ItemDescription("An egg dish that looks and tastes as fluffy as a cloud."), ItemCategory(ItemCategory.Food), SerializedAsUnicode]
    EggSoufflé,

    [ItemName("Exquisite Steak"), ItemDescription("Steak made with the choicest meat. Meals like this can change your life."), ItemCategory(ItemCategory.Food)]
    ExquisiteSteak,

    [ItemName("Fish & Chips"), ItemDescription("Fried fish and fried potatoes on a single plate.  Fattening? Never."), ItemCategory(ItemCategory.Food)]
    FishAndChips,

    [ItemName("Fish Hot Pot"), ItemDescription("A hot pot made with whitefish. Add rice at the end for a risotto."), ItemCategory(ItemCategory.Food)]
    FishHotPot,

    [ItemName("Flan"), ItemDescription("A steamed egg-and-milk custard. Every bite is bliss."), ItemCategory(ItemCategory.Food)]
    Flan,

    [ItemName("Forneus in Garlic"), ItemDescription("Whitefish fried in garlic and fat. The smell clings to your garments."), ItemCategory(ItemCategory.Food)]
    ForneusinGarlic,

    [ItemName("Forneus Meunière"), ItemDescription("Whitefish fried in a bit of fat until it turns a scrumptious golden brown."), ItemCategory(ItemCategory.Food), SerializedAsUnicode]
    ForneusMeunière,

    [ItemName("Fried Egg"), ItemDescription("The simplest of egg dishes. Needs no more than a dash of salt and pepper."), ItemCategory(ItemCategory.Food)]
    FriedEgg,

    [ItemName("Fried Fish"), ItemDescription("Whitefish that has been breaded and fried to enhance flavor and texture."), ItemCategory(ItemCategory.Food)]
    FriedFish,

    [ItemName("Fried Potatoes"), ItemDescription("Potatoes fried in oil, which gets all over your hands... You won't care."), ItemCategory(ItemCategory.Food)]
    FriedPotatoes,

    [ItemName("Fruit Juice"), ItemDescription("Nutrient-rich juice. Drink in moderation to avoid a bellyache."), ItemCategory(ItemCategory.Food)]
    FruitJuice,

    [ItemName("Garlic Chicken"), ItemDescription("Chicken cooked in garlic sauce. One whiff is enough to make you hungry."), ItemCategory(ItemCategory.Food)]
    GarlicChicken,

    [ItemName("Ginger Pork"), ItemDescription("Thinly sliced pork simmered in ginger sauce.  Pairs well with rice."), ItemCategory(ItemCategory.Food)]
    GingerPork,

    [ItemName("Lemonade"), ItemDescription("A refreshing, summery drink made from sweetened  lemon juice."), ItemCategory(ItemCategory.Food)]
    Lemonade,

    [ItemName("Lemon Cream Pie"), ItemDescription("A summer treat made with a lemon juice filling and  topped with cream."), ItemCategory(ItemCategory.Food)]
    LemonPie,

    [ItemName("Macaroni & Cheese"), ItemDescription("A classic made by slathering pasta with cheese.  But this seems off..."), ItemCategory(ItemCategory.Food)]
    MacaroniAndCheese,

    [ItemName("Macaroni Gratin"), ItemDescription("Macaroni covered in white sauce and baked. Don't miss the crispy bottom."), ItemCategory(ItemCategory.Food)]
    MacaroniGratin,

    [ItemName("Manju"), ItemDescription("An Eastern afternoon treat made of bean paste wrapped in dough."), ItemCategory(ItemCategory.Food)]
    Manju,

    [ItemName("Meat Hot Pot"), ItemDescription("An invigorating hot pot brimming with slices of meat."), ItemCategory(ItemCategory.Food)]
    MeatHotPot,

    [ItemName("Miso Cutlet"), ItemDescription("A pork cutlet topped with miso. Popular in some parts of Nippon."), ItemCategory(ItemCategory.Food)]
    MisoCutlet,

    [ItemName("Miso Ramen"), ItemDescription("Chinese noodles in miso broth. Warms the body and soul."), ItemCategory(ItemCategory.Food)]
    MisoRamen,

    [ItemName("Nectar"), ItemDescription("The libation of the gods. Grants immortality and staves off wrinkles."), ItemCategory(ItemCategory.Food)]
    Nectar,

    [ItemName("Omurice"), ItemDescription("Rice wrapped in an omelette. A novel idea, but tricky to prepare."), ItemCategory(ItemCategory.Food)]
    Omurice,

    [ItemName("Pasta Carbonara"), ItemDescription("A popular pasta made with a thick, creamy sauce. Quick and delicious."), ItemCategory(ItemCategory.Food)]
    PastaCarbonara,

    [ItemName("Pasta Vongole"), ItemDescription("A clam and garlic pasta that floods your mouth with flavor."), ItemCategory(ItemCategory.Food)]
    PastaVongole,

    [ItemName("Pizza"), ItemDescription("Pizza dough covered in toppings and an irresistible layer of cheese."), ItemCategory(ItemCategory.Food)]
    Pizza,

    [ItemName("Pork Curry"), ItemDescription("A curry and rice plate with large chunks of pork. Tasty and filling."), ItemCategory(ItemCategory.Food)]
    PorkCurry,

    [ItemName("Rice Ball"), ItemDescription("Cooked rice shaped into a portable snack for when you feel peckish."), ItemCategory(ItemCategory.Food)]
    RiceBall,

    [ItemName("Rolled Omelette"), ItemDescription("A whisked eggs spread flat while cooking and then rolled up."), ItemCategory(ItemCategory.Food)]
    RolledOmelette,

    [ItemName("Salt Ramen"), ItemDescription("Chinese noodles in salt broth. Goes well with a bowl of rice."), ItemCategory(ItemCategory.Food)]
    SaltRamen,

    [ItemName("Scrambled Eggs"), ItemDescription("A dish made by whisking eggs before cooking them. Or, a failed omelette."), ItemCategory(ItemCategory.Food)]
    ScrambledEggs,

    [ItemName("Seafood Curry"), ItemDescription("Curry and rice with seafood that will make waves in your mouth."), ItemCategory(ItemCategory.Food)]
    SeafoodCurry,

    [ItemName("Sea Urchin Pasta"), ItemDescription("Pasta tossed in creamy sea urchin and accented with an acidic tomato note."), ItemCategory(ItemCategory.Food)]
    SeaUrchinPasta,

    [ItemName("Shiruko Spaghetti"), ItemDescription("A chance creation made with sweet red bean juice. Not for everyone..."), ItemCategory(ItemCategory.Food)]
    ShirukoSpaghetti,

    [ItemName("Simmered Forneus"), ItemDescription("Simmered whitefish that falls apart in your mouth."), ItemCategory(ItemCategory.Food)]
    SimmeredForneus,

    [ItemName("Smoothie"), ItemDescription("Fruit blended into a smooth drink. A recipe for beautiful skin."), ItemCategory(ItemCategory.Food)]
    Smoothie,

    [ItemName("Soy Ramen"), ItemDescription("Chinese noodles in soy broth. It's polite to drink all the soup."), ItemCategory(ItemCategory.Food)]
    SoyRamen,

    [ItemName("Sponge Cake"), ItemDescription("A fluffy cake that rises in the oven and tastes great all on its own."), ItemCategory(ItemCategory.Food)]
    SpongeCake,

    [ItemName("Steak"), ItemDescription("A slice of meat cooked on a hot surface.  You'll savor every bite."), ItemCategory(ItemCategory.Food)]
    Steak,

    [ItemName("Strawberry Au Lait"), ItemDescription("A glass of milk with strawberries in it. They always sink to the bottom..."), ItemCategory(ItemCategory.Food)]
    StrawberryAuLait,

    [ItemName("Strawberry Cake"), ItemDescription("A cake containing strawberries (which discerning eaters save for last)."), ItemCategory(ItemCategory.Food)]
    StrawberryCake,

    [ItemName("Strawberry Crepe"), ItemDescription("Cream and strawberries wrapped in a crepe for a delightful combination."), ItemCategory(ItemCategory.Food)]
    StrawberryCrepe,

    [ItemName("Strawberry Pie"), ItemDescription("A pie encrusted with strawberries. It's as pretty as jewelry."), ItemCategory(ItemCategory.Food)]
    StrawberryPie,

    [ItemName("Sukiyaki"), ItemDescription("A hot pot made with meat and vegetables which are dipped in egg."), ItemCategory(ItemCategory.Food)]
    Sukiyaki,

    [ItemName("Pork Cutlet"), ItemDescription("A slice of pork that has been breaded and fried for  a crispy outside."), ItemCategory(ItemCategory.Food)]
    Tonkatsu,

    [ItemName("Tonkotsu Ramen"), ItemDescription("Chinese noodles in tonkotsu broth. A rich flavor you'll learn to crave."), ItemCategory(ItemCategory.Food)]
    TonkotsuRamen,

    [ItemName("Uni Rice Bowl"), ItemDescription("A simple dish consisting of sea urchin on cooked rice. Delectable."), ItemCategory(ItemCategory.Food)]
    UniDon,

    [ItemName("Corn Seed"), ItemDescription("Corn that has been kept for planting. You could use it to create more corn."), ItemCategory(ItemCategory.Seed)]
    SeedCorn,

    [ItemName("Potato Seed"), ItemDescription("A potato that has been kept for planting. Salt allows you to grow a larger crop."), ItemCategory(ItemCategory.Seed)]
    SeedPotato,

    [ItemName("Rice Seed"), ItemDescription("Rice that has been kept for planting. You could use it to create more rice."), ItemCategory(ItemCategory.Seed)]
    SeedRice,

    [ItemName("Steel Equipment/R"), ItemDescription("A recipe for steel equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe003,

    [ItemName("Obsidian Equipment/R"), ItemDescription("A recipe for obsidian equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe004,

    [ItemName("Damascus Equipment/R"), ItemDescription("A recipe for Damascus equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe005,

    [ItemName("Gold Equipment/R"), ItemDescription("A recipe for gold equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe007,

    [ItemName("Crimsonite Equipment/R"), ItemDescription("A recipe for crimsonite equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe009,

    [ItemName("Cashmere Equipment/R"), ItemDescription("A recipe for cashmere equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe012,

    [ItemName("Fine Equipment/R"), ItemDescription("A recipe for fine equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe018,

    [ItemName("Very Fine Equipment/R"), ItemDescription("A recipe for very fine equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe019,

    [ItemName("Legendary Equipment/R"), ItemDescription("A recipe for legendary equipment."), ItemCategory(ItemCategory.Key)]
    ArmsRecipe020,

    [ItemName("Elemental Ammunition/R"), ItemDescription("A recipe for ammunition imbued with elemental magic."), ItemCategory(ItemCategory.Key)]
    BalletRecipe001,

    [ItemName("Special Ammunition/R"), ItemDescription("A recipe for ammunition with special effects."), ItemCategory(ItemCategory.Key)]
    BalletRecipe002,

    [ItemName("Potent Ammunition/R"), ItemDescription("A recipe for potent ammunition."), ItemCategory(ItemCategory.Key)]
    BalletRecipe003,

    [ItemName("Ultimate Ammunition/R"), ItemDescription("A recipe for the ultimate ammunition. Uses diamond."), ItemCategory(ItemCategory.Key)]
    BalletRecipe004,

    [ItemName("Passplate"), ItemDescription("A relic required to pass through the demons' entry gate."), ItemCategory(ItemCategory.Key)]
    Certificationboard,

    [ItemName("Discount Card"), ItemDescription("A card given to the supply post's loyal customers. Allows you to purchase items at a discount."), ItemCategory(ItemCategory.Key)]
    DiscountCard,

    [ItemName("Ramen/R"), ItemDescription("A recipe for ramen."), ItemCategory(ItemCategory.Key)]
    DishRecipe002,

    [ItemName("Curry Dish/R"), ItemDescription("A recipe for a curry dish."), ItemCategory(ItemCategory.Key)]
    DishRecipe003,

    [ItemName("Pasta Dish/R"), ItemDescription("A recipe for a pasta dish."), ItemCategory(ItemCategory.Key)]
    DishRecipe004,

    [ItemName("Meat Dish/R"), ItemDescription("A recipe for a meat dish."), ItemCategory(ItemCategory.Key)]
    DishRecipe005,

    [ItemName("Fish Dish/R"), ItemDescription("A recipe for a fish dish."), ItemCategory(ItemCategory.Key)]
    DishRecipe006,

    [ItemName("Sweets/R"), ItemDescription("A recipe for sweets."), ItemCategory(ItemCategory.Key)]
    DishRecipe007,

    [ItemName("Cookies/R"), ItemDescription("A recipe for cookies."), ItemCategory(ItemCategory.Key)]
    DishRecipe008,

    [ItemName("Cake/R"), ItemDescription("A recipe for cake."), ItemCategory(ItemCategory.Key)]
    DishRecipe010,

    [ItemName("Drink/R"), ItemDescription("A recipe for a drink."), ItemCategory(ItemCategory.Key)]
    DishRecipe011,

    [ItemName("Ultimate Dish/R"), ItemDescription("A recipe for the \"ultimate dish.\" Allegedly more delicious than the \"supreme dish.\""), ItemCategory(ItemCategory.Key)]
    DishRecipe014,

    [ItemName("Supreme Dish/R"), ItemDescription("A recipe for the \"supreme dish.\" Allegedly more delicious than the \"ultimate dish.\""), ItemCategory(ItemCategory.Key)]
    DishRecipe015,

    [ItemName("Identification"), ItemDescription("The latest in ecclesiastical research: a rather hazy \"photograph.\""), ItemCategory(ItemCategory.Key)]
    IDphoto,

    [ItemName("Fine Healing Item/R"), ItemDescription("A recipe for a fine healing item."), ItemCategory(ItemCategory.Key)]
    ItemRecipe001,

    [ItemName("Ultimate Healing Item/R"), ItemDescription("A recipe for the ultimate healing item."), ItemCategory(ItemCategory.Key)]
    ItemRecipe002,

    [ItemName("Faerie Healing Item/R"), ItemDescription("A recipe for a faerie healing item."), ItemCategory(ItemCategory.Key)]
    ItemRecipe003,

    [ItemName("Carpenter's Key"), ItemDescription("Unlocks a special room, inside which a demon awaits your challenge."), ItemCategory(ItemCategory.Key)]
    Keyofbacker1,

    [ItemName("Warhorse's Key"), ItemDescription("Unlocks a special room, inside which a demon awaits your challenge."), ItemCategory(ItemCategory.Key)]
    Keyofbacker2,

    [ItemName("Millionaire's Key"), ItemDescription("Unlocks a special room, inside which a demon awaits your challenge."), ItemCategory(ItemCategory.Key)]
    Keyofbacker3,

    [ItemName("Celeste's Key"), ItemDescription("Unlock Celeste's room."), ItemCategory(ItemCategory.Key)]
    Keyofbacker4,

    [ItemName("Vepar Medal"), ItemDescription("Proof that you have triumphed over Vepar."), ItemCategory(ItemCategory.Key)]
    Medal001,

    [ItemName("Zangetsu Medal"), ItemDescription("Proof that you have triumphed over Zangetsu."), ItemCategory(ItemCategory.Key)]
    Medal002,

    [ItemName("Craftwork Medal"), ItemDescription("Proof that you have triumphed over the Craftwork."), ItemCategory(ItemCategory.Key)]
    Medal003,

    [ItemName("Andrealphus Medal"), ItemDescription("Proof that you have triumphed over Andrealphus."), ItemCategory(ItemCategory.Key)]
    Medal004,

    [ItemName("Glutton Train Medal"), ItemDescription("Proof that you have triumphed over the Glutton Train."), ItemCategory(ItemCategory.Key)]
    Medal005,

    [ItemName("Valac Medal"), ItemDescription("Proof that you have triumphed over Valac."), ItemCategory(ItemCategory.Key)]
    Medal006,

    [ItemName("Bathin Medal"), ItemDescription("Proof that you have triumphed over Bathin."), ItemCategory(ItemCategory.Key)]
    Medal007,

    [ItemName("Abyssal Guardian Medal"), ItemDescription("Proof that you have triumphed over the Abyssal Guardian."), ItemCategory(ItemCategory.Key)]
    Medal008,

    [ItemName("Gebel Medal"), ItemDescription("Proof that you have triumphed over Gebel."), ItemCategory(ItemCategory.Key)]
    Medal009,

    [ItemName("Bloodless Medal"), ItemDescription("Proof that you have triumphed over Bloodless."), ItemCategory(ItemCategory.Key)]
    Medal010,

    [ItemName("Alfred Medal"), ItemDescription("Proof that you have triumphed over Alfred."), ItemCategory(ItemCategory.Key)]
    Medal011,

    [ItemName("Orobas Medal"), ItemDescription("Proof that you have triumphed over Orobas."), ItemCategory(ItemCategory.Key)]
    Medal012,

    [ItemName("True Zangetsu Medal"), ItemDescription("Proof that you have triumphed over Zangetsu at his most powerful."), ItemCategory(ItemCategory.Key)]
    Medal013,

    [ItemName("Valefar Medal"), ItemDescription("Proof that you have triumphed over Valefar."), ItemCategory(ItemCategory.Key)]
    Medal014,

    [ItemName("Gremory Medal"), ItemDescription("Proof that you have triumphed over Gremory."), ItemCategory(ItemCategory.Key)]
    Medal015,

    [ItemName("Dominique Medal"), ItemDescription("Proof that you have triumphed over Dominique."), ItemCategory(ItemCategory.Key)]
    Medal016,

    [ItemName("Bael Medal"), ItemDescription("Proof that you have triumphed over Bael."), ItemCategory(ItemCategory.Key)]
    Medal017,

    [ItemName("O.D. Medal"), ItemDescription("Proof that you have triumphed over Orlok Dracule."), ItemCategory(ItemCategory.Key)]
    Medal018,

    [ItemName("Unused"), ItemDescription("Unused"), ItemCategory(ItemCategory.Key)]
    Medal019,

    [ItemName("IGA Medal"), ItemDescription("Proof that you have triumphed over IGA."), ItemCategory(ItemCategory.Key)]
    Medal020,

    [ItemName("8-bit Medal"), ItemDescription("Proof that you have triumphed over the Eight Bit Overlord."), ItemCategory(ItemCategory.Key)]
    Medal021,

    [ItemName("Doppelganger Medal"), ItemDescription("Proof that you have triumphed over the Doppelganger."), ItemCategory(ItemCategory.Key)]
    Medal023,

    [ItemName("Galleon Map"), ItemDescription("A map of the Minerva."), ItemCategory(ItemCategory.Key)]
    ShipMap,

    [ItemName("Silver Bromide"), ItemDescription("A special mineral required to produce \"photographs\"."), ItemCategory(ItemCategory.Key)]
    SilverBromide,

    [ItemName("Village Key"), ItemDescription("A key that opens the locked door in the shelter."), ItemCategory(ItemCategory.Key)]
    VillageKey,

    [ItemName("Hair Apparent I"), ItemDescription("A magazine issue that glamorizes the \"French braid.\""), ItemCategory(ItemCategory.Key)]
    Worldfashionfirstissue,

    [ItemName("Hair Apparent II"), ItemDescription("A magazine issue that glamorizes \"swept-back\" hairstyles."), ItemCategory(ItemCategory.Key)]
    WorldfashionNo02,

    [ItemName("Hair Apparent III"), ItemDescription("A magazine issue that glamorizes \"hair bunches.\""), ItemCategory(ItemCategory.Key)]
    WorldfashionNo03,

    [ItemName("Hair Apparent IV"), ItemDescription("A magazine issue that glamorizes the \"double bun.\""), ItemCategory(ItemCategory.Key)]
    WorldfashionNo04,

    [ItemName("Hair Apparent V"), ItemDescription("A magazine issue that glamorizes \"straight long\" hairstyles."), ItemCategory(ItemCategory.Key)]
    WorldfashionNo05,

    [ItemName("Hair Apparent VI"), ItemDescription("A magazine issue that glamorizes \"boyish\" hairstyles."), ItemCategory(ItemCategory.Key)]
    WorldfashionNo06,

    [ItemName("Hair Apparent VII"), ItemDescription("A magazine issue that glamorizes the \"ponytail.\""), ItemCategory(ItemCategory.Key)]
    WorldfashionNo07,

    [ItemName("Hair Apparent VIII"), ItemDescription("A magazine issue that glamorizes \"geisha\" hairstyles."), ItemCategory(ItemCategory.Key)]
    WorldfashionNo08,

    [ItemName("Hair Apparent IX"), ItemDescription("A magazine issue that glamorizes \"braided bangs.\""), ItemCategory(ItemCategory.Key)]
    WorldfashionNo09,

    [ItemName("Hair Apparent X"), ItemDescription("A magazine issue that glamorizes \"vintage curls.\""), ItemCategory(ItemCategory.Key)]
    WorldfashionNo10,

    [ItemName("Hair Apparent XI"), ItemDescription("A magazine issue that glamorizes \"high forehead\" hairstyles."), ItemCategory(ItemCategory.Key)]
    WorldfashionNo11,

    [ItemName("Hair Apparent XII"), ItemDescription("A magazine issue that glamorizes \"valkyrie\" hairstyles."), ItemCategory(ItemCategory.Key)]
    WorldfashionNo12,

    [ItemName("Tailwind Tome"), ItemDescription("A book that fortifies the legs. Increases movement speed."), ItemCategory(ItemCategory.Book)]
    Abookofthegale,

    [ItemName("Sage's Tome"), ItemDescription("A book imbued with magic that increases intelligence (INT)."), ItemCategory(ItemCategory.Book)]
    Abookofwisemen,

    [ItemName("Immunity Tome"), ItemDescription("A book imbued with fortifying magic. Increases poison resistance."), ItemCategory(ItemCategory.Book)]
    Bookofdetoxification,

    [ItemName("Deadeye Tome"), ItemDescription("A book written by a godlike hunter. Makes directional shards stronger."), ItemCategory(ItemCategory.Book)]
    BookofDevils,

    [ItemName("Mastery Tome"), ItemDescription("A book by a warrior of great skill. Hastens proficiency growth."), ItemCategory(ItemCategory.Book)]
    Bookofexercise,

    [ItemName("Believer's Tome"), ItemDescription("A book imbued with magic that increases mind (MND)."), ItemCategory(ItemCategory.Book)]
    Bookoffaith,

    [ItemName("Fortune Tome"), ItemDescription("A curious book that seems to bring good fortune. Increases LUCK."), ItemCategory(ItemCategory.Book)]
    BookofFortune,

    [ItemName("Corporeity Tome"), ItemDescription("A book holding secrets of the flesh. Increases petrification resistance."), ItemCategory(ItemCategory.Book)]
    Bookoffossilstone,

    [ItemName("Godsend Tome"), ItemDescription("A marvelous book that brings wondrous fortune. Greatly increases LUCK."), ItemCategory(ItemCategory.Book)]
    BookofFury,

    [ItemName("Empyreal Tome"), ItemDescription("A holy book containing God's power. Increases resistance to darkness."), ItemCategory(ItemCategory.Book)]
    Bookoflatency,

    [ItemName("Might Tome"), ItemDescription("A book imbued with magic that increases strength (STR)."), ItemCategory(ItemCategory.Book)]
    Bookofstiffness,

    [ItemName("Tactician's Tome"), ItemDescription("A book of power by a great tactician. Increases EXP intake."), ItemCategory(ItemCategory.Book)]
    BookofTactics,

    [ItemName("Tome of Conquest"), ItemDescription("The ultimate tome. Equivalent to the Prowess, Tailwind, and Mastery Tomes."), ItemCategory(ItemCategory.Book)]
    Bookofthechampion,

    [ItemName("Ancillary Tome"), ItemDescription("A book by a legendary summoner. Increases the power of familiars."), ItemCategory(ItemCategory.Book)]
    Bookofthefollower,

    [ItemName("Prowess Tome"), ItemDescription("A book that speeds muscle movement. Increases standard attack speed."), ItemCategory(ItemCategory.Book)]
    BookoftheMighty,

    [ItemName("Coldstave Tome"), ItemDescription("A book that is warm to the touch. Increases resistance to ice."), ItemCategory(ItemCategory.Book)]
    Bookofwinter,

    [ItemName("Sentinel Tome"), ItemDescription("A book imbued with magic that increases constitution (CON)."), ItemCategory(ItemCategory.Book)]
    GuardiansBook,

    [ItemName("Heatstave Tome"), ItemDescription("A book that is cold to the touch. Increases resistance to fire."), ItemCategory(ItemCategory.Book)]
    Heatresistantbook,

    [ItemName("Shockstave Tome"), ItemDescription("A book under divine protection. Increases resistance to thunder."), ItemCategory(ItemCategory.Book)]
    LightningProtectionBook,

    [ItemName("Obscurity Tome"), ItemDescription("A book brimming with dark energy. Increases resistance to light."), ItemCategory(ItemCategory.Book)]
    ShadingBook,

    [ItemName("Blessed Tome"), ItemDescription("A book that a priest has blessed. Increases curse resistance."), ItemCategory(ItemCategory.Book)]
    TheBookofMonster,

    [ItemName("AP Rounds"), ItemDescription("Armor-piercing rounds with sharp points that  penetrate targets."), ItemCategory(ItemCategory.Bullet)]
    ArmorPiercing,

    [ItemName("Infinite Rounds"), ItemDescription("Magical bullets that never deplete. However, they are quite weak."), ItemCategory(ItemCategory.Bullet)]
    Bullet_Defaultbullet,

    [ItemName("Critical Rounds"), ItemDescription("Bullets that fragment on impact, dealing frequent critical hits."), ItemCategory(ItemCategory.Bullet)]
    CriticalBullet,

    [ItemName("Curse Rounds"), ItemDescription("Bullets that carry a curse."), ItemCategory(ItemCategory.Bullet)]
    CurseRounds,

    [ItemName("Diamond Bullets"), ItemDescription("Potent ammunition made from the hardest substance on earth."), ItemCategory(ItemCategory.Bullet)]
    DiamondBullets,

    [ItemName("Flame Rounds"), ItemDescription("Bullets imbued with fire magic."), ItemCategory(ItemCategory.Bullet)]
    Firebullet,

    [ItemName("HP Rounds"), ItemDescription("Hollow-point rounds used in hunting and good for inflicting damage."), ItemCategory(ItemCategory.Bullet)]
    Hollowpoint,

    [ItemName("Ice Rounds"), ItemDescription("Bullets imbued with ice magic."), ItemCategory(ItemCategory.Bullet)]
    Icebullet,

    [ItemName("Petrifying Rounds"), ItemDescription("Bullets capable of turning targets to stone."), ItemCategory(ItemCategory.Bullet)]
    PetrifyingRounds,

    [ItemName("Poison Rounds"), ItemDescription("Bullets with poison-laced tips."), ItemCategory(ItemCategory.Bullet)]
    PoisonRounds,

    [ItemName("Shieldbane Rounds"), ItemDescription("Special bullets that lower the defensive capabilities of targets."), ItemCategory(ItemCategory.Bullet)]
    ShieldbaneRounds,

    [ItemName("Scattershot"), ItemDescription("Ammunition that fires in a wide spread."), ItemCategory(ItemCategory.Bullet)]
    Shotshell,

    [ItemName("Silver Bullets"), ItemDescription("Silver bullets that are especially effective against the forces of evil."), ItemCategory(ItemCategory.Bullet)]
    SilverBullets,

    [ItemName("SP Rounds"), ItemDescription("Soft-point rounds with no penetrative capability."), ItemCategory(ItemCategory.Bullet)]
    Softpoint,

    [ItemName("Thunder Rounds"), ItemDescription("Bullets imbued with thunder magic."), ItemCategory(ItemCategory.Bullet)]
    ThunderRounds,

    [ItemName("Weaponbane Rounds"), ItemDescription("Special bullets that lower the offensive capabilities of targets."), ItemCategory(ItemCategory.Bullet)]
    WeaponbaneRounds,

    [ItemName("Axe Strike"), ItemDescription("Produce a giant axe and strike the enemy with a grievous blow."), ItemCategory(ItemCategory.ConjureShards)]
    AxStrike,

    [ItemName("Blood Steal"), ItemDescription("Absorb blood from nearby enemies to restore your health."), ItemCategory(ItemCategory.ConjureShards)]
    BloodSteel,

    [ItemName("Cerulean Splash"), ItemDescription("Launch a bouncing ball of water at enemies."), ItemCategory(ItemCategory.ConjureShards)]
    Ceruleansplash,

    [ItemName("Death Cry"), ItemDescription("Regale your surroundings with a chilling and damaging cry."), ItemCategory(ItemCategory.ConjureShards)]
    Deadhowling,

    [ItemName("Draconic Rage"), ItemDescription("Rend enemies with a dragon's claws."), ItemCategory(ItemCategory.ConjureShards)]
    DragonicRage,

    [ItemName("Dream Steal"), ItemDescription("Drain energy from deranged enemies."), ItemCategory(ItemCategory.ConjureShards)]
    DREAMSTEAL,

    [ItemName("Flytrap"), ItemDescription("Place a moco weed on the ground."), ItemCategory(ItemCategory.ConjureShards)]
    EntangleBind,

    [ItemName("Gen"), ItemDescription("Regale your surroundings with a chilling and damaging cry."), ItemCategory(ItemCategory.ConjureShards)]
    Gen,

    [ItemName("Summon 8-bit Ghost"), ItemDescription("Call forth an eight-bit ghost."), ItemCategory(ItemCategory.ConjureShards)]
    Ghost8Bit,

    [ItemName("Welcome Company"), ItemDescription("Summons Poltergeist to create a protection barrier."), ItemCategory(ItemCategory.ConjureShards)]
    GratefulAssist,

    [ItemName("Head Flail"), ItemDescription("Attack by using a dullahammer's head as a flail."), ItemCategory(ItemCategory.ConjureShards)]
    HEADFAIL,

    [ItemName("Summon Hellhound"), ItemDescription("Call forth a hellhound and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    HellHound,

    [ItemName("Shuriken"), ItemDescription("Attack with a type of concealed throwing weapon."), ItemCategory(ItemCategory.ConjureShards)]
    HiddenDart,

    [ItemName("Jackpot"), ItemDescription("Use a slot machine to determine the nature of your attack."), ItemCategory(ItemCategory.ConjureShards)]
    Jackpot,

    [ItemName("Riga Storæma"), ItemDescription("Conjure a column of flame."), ItemCategory(ItemCategory.ConjureShards)]
    LigaStreyma,

    [ItemName("Insatiable"), ItemDescription("Shatter glass on the ground to create a pool of magic."), ItemCategory(ItemCategory.ConjureShards)]
    NeverSatisfied,

    [ItemName("8-bit Flame"), ItemDescription("Produce a pillar of eight-bit flame."), ItemCategory(ItemCategory.ConjureShards)]
    Nightmare8Bit,

    [ItemName("Oengus"), ItemDescription("Increases damage from counter attack."), ItemCategory(ItemCategory.ConjureShards)]
    Oengus,

    [ItemName("Gale Crawler"), ItemDescription("Create a shockwave that travels across the ground and rips enemies apart."), ItemCategory(ItemCategory.ConjureShards)]
    Raginggirl,

    [ItemName("Throw Spear"), ItemDescription("Throw a spear at foes."), ItemCategory(ItemCategory.ConjureShards)]
    RapidSpear,

    [ItemName("Robert"), ItemDescription("Call forth an ape-like demon and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    Robert,

    [ItemName("Rubella"), ItemDescription("Send rhythmic blasts of flame toward foes."), ItemCategory(ItemCategory.ConjureShards)]
    Rubella,

    [ItemName("Sakura Storm"), ItemDescription("Meditate on the transcience of life as enemies are overwhelmed with ailments."), ItemCategory(ItemCategory.ConjureShards)]
    SakuraRain,

    [ItemName("Summon Water Leaper"), ItemDescription("Call forth an amphibious demon and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SamonSamHiggin,

    [ItemName("Summon Shovel Armor"), ItemDescription("Call forth a shovel-wielding knight and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    Shovelshooter,

    [ItemName("Summon Ghost"), ItemDescription("Call forth a ghost and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SummonAme,

    [ItemName("Summon Simian"), ItemDescription("Call forth an ape-like demon and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SummonApe,

    [ItemName("Summon Bu-chan"), ItemDescription("Bu-chan appears in the nick of time to pull Miri out of another jam."), ItemCategory(ItemCategory.ConjureShards)]
    SummonBuChan,

    [ItemName("Summon Buer"), ItemDescription("Call forth a leonine demon and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SummonBuell,

    [ItemName("Summon Insect"), ItemDescription("Call forth an insect that attacks diagonally forward."), ItemCategory(ItemCategory.ConjureShards)]
    SummonBugs,

    [ItemName("Summon Bat"), ItemDescription("Call forth a bat and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SummonButt,

    [ItemName("Summon Chair"), ItemDescription("Call forth a...chair? and set it down."), ItemCategory(ItemCategory.ConjureShards)]
    SummonChair,

    [ItemName("Summon Gieremund"), ItemDescription("Call forth a demon hound and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SummonGilemund,

    [ItemName("Summon Rat"), ItemDescription("Call forth a giant rat and set it upon foes."), ItemCategory(ItemCategory.ConjureShards)]
    SummonRat,

    [ItemName("Summon Tracer"), ItemDescription("Call forth a fiend that lies in wait beneath the terrain."), ItemCategory(ItemCategory.ConjureShards)]
    SummonTracer,

    [ItemName("Thrashing Tentacle"), ItemDescription("Attack foes with one of Vepar's tentacles."), ItemCategory(ItemCategory.ConjureShards)]
    SwingTentacle,

    [ItemName("Teps Salrenda"), ItemDescription("Strike your surroundings with a merciless barrage of lightning."), ItemCategory(ItemCategory.ConjureShards)]
    TepsSalenda,

    [ItemName("Throwing Axe"), ItemDescription("Throw a spinning axe."), ItemCategory(ItemCategory.ConjureShards)]
    ThrowAxe,

    [ItemName("Tis Raiff"), ItemDescription("Unleash holy rays."), ItemCategory(ItemCategory.ConjureShards)]
    TissLeif,

    [ItemName("Toxic Storm"), ItemDescription("Whip up a poisonous whirlwind in front of you."), ItemCategory(ItemCategory.ConjureShards)]
    Toxicstorm,

    [ItemName("Upbeat Heat"), ItemDescription("Send rhythmic blasts of flame toward foes."), ItemCategory(ItemCategory.ConjureShards)]
    Upbeatheat,

    [ItemName("Va Ischa"), ItemDescription("Fling a sharp sliver of ice toward enemies."), ItemCategory(ItemCategory.ConjureShards)]
    VaIsha,

    [ItemName("Va Schia"), ItemDescription("Summon a hunk of ice and drop it on enemies."), ItemCategory(ItemCategory.ConjureShards)]
    VaSka,

    [ItemName("Summon 8-bit Zombie"), ItemDescription("Call forth an eight-bit zombie."), ItemCategory(ItemCategory.ConjureShards)]
    Zombie8Bit,

    [ItemName("Accelerator"), ItemDescription("Move more quickly."), ItemCategory(ItemCategory.ManipulativeShards)]
    Accelerator,

    [ItemName("Standstill"), ItemDescription("Project a field that stops time."), ItemCategory(ItemCategory.ManipulativeShards)]
    AccelWorld,

    [ItemName("Beast Guardian"), ItemDescription("Summon a wolfman to guard you from frontal attacks."), ItemCategory(ItemCategory.ManipulativeShards)]
    Beastguard,

    [ItemName("Bunnymorphosis"), ItemDescription("Transform into a lili."), ItemCategory(ItemCategory.ManipulativeShards)]
    ChangeBunny,

    [ItemName("Craftwork"), ItemDescription("Grab on to certain objects and manipulate them."), ItemCategory(ItemCategory.ManipulativeShards)]
    Demoniccapture,

    [ItemName("Healing"), ItemDescription("Gradually recover HP while the shard is active."), ItemCategory(ItemCategory.ManipulativeShards)]
    Healing,

    [ItemName("Sacred Shade"), ItemDescription("Temporarily increase your stats."), ItemCategory(ItemCategory.ManipulativeShards)]
    Sacredshade,

    [ItemName("Shadow Tracer"), ItemDescription("Create a shadow double."), ItemCategory(ItemCategory.ManipulativeShards)]
    Shadowtracer,

    [ItemName("Feral Claws"), ItemDescription("Attack with sharp, lupine claws."), ItemCategory(ItemCategory.ManipulativeShards)]
    WildScratch,

    [ItemName("Acid Jet"), ItemDescription("Spray a powerful acid in the designated direction. "), ItemCategory(ItemCategory.DirectionalShards)]
    Acidgouache,

    [ItemName("Directed Shield"), ItemDescription("Call forth a shield that can be faced in any direction."), ItemCategory(ItemCategory.DirectionalShards)]
    Aimingshield,

    [ItemName("Aqua Stream"), ItemDescription("Allows you to swim by launching water in the designated direction. MP is not consumed underwater."), ItemCategory(ItemCategory.DirectionalShards)]
    Aquastream,

    [ItemName("Bolide Blast"), ItemDescription("Cause an explosion in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    BaudRideBlast,

    [ItemName("Bone Toss"), ItemDescription("Throw a bone in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    BoonsLore,

    [ItemName("Chaser Arrow"), ItemDescription("Shoot a homing arrow in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    ChaseArrow,

    [ItemName("Chisel Barrage"), ItemDescription("Launch a series of chisels."), ItemCategory(ItemCategory.DirectionalShards)]
    Chiselbalage,

    [ItemName("Circular Ripper"), ItemDescription("Swing a sharp, rotating disc in the designated direction. "), ItemCategory(ItemCategory.DirectionalShards)]
    CircleRipper,

    [ItemName("Lethargy"), ItemDescription("Fire a beam in the designated direction that slows enemy movement."), ItemCategory(ItemCategory.DirectionalShards)]
    CurseDray,

    [ItemName("Dimension Shift"), ItemDescription("Warp a short distance in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    DimensionShift,

    [ItemName("8-bit Fire Ball"), ItemDescription("Throw an eight bit fire ball in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    EightBitFire,

    [ItemName("Half-Genie Fireball"), ItemDescription("Popular and powerful Sequin Land magic."), ItemCategory(ItemCategory.DirectionalShards)]
    Fireball,

    [ItemName("Flame Cannon"), ItemDescription("Launch a fireball in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    FireCannon,

    [ItemName("Flamethrower"), ItemDescription("Launch flames in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    FireThrower,

    [ItemName("Fald Taiab"), ItemDescription("Create a field of dark power in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    FoldingTurb,

    [ItemName("Fald Ciu"), ItemDescription("Launch a focused ball of dark energy in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    FoldShiu,

    [ItemName("Gold Bullet"), ItemDescription("Weaponize 1% of your total gold and throw it in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    GoldBarrett,

    [ItemName("Hammer Knuckle"), ItemDescription("Swing a massive fist in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    Hammerknuckle,

    [ItemName("Heretical Grinder"), ItemDescription("Extend a shaft of rotating blades in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    HelleTicalGrinder,

    [ItemName("Inferno Breath"), ItemDescription("Expel flames in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    InfernoBrace,

    [ItemName("Riga Dohin"), ItemDescription("Create a field of incinerating force."), ItemCategory(ItemCategory.DirectionalShards)]
    LigaDoin,

    [ItemName("Light Ray"), ItemDescription("Hold RT to target enemies,  release to blast them with radiant light."), ItemCategory(ItemCategory.DirectionalShards)]
    LightRay,

    [ItemName("Petra Breath"), ItemDescription("Release a gas that causes petrification in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    PetraBless,

    [ItemName("Petra Ray"), ItemDescription("Fire a beam in the designated direction that petrifies enemies."), ItemCategory(ItemCategory.DirectionalShards)]
    Petrey,

    [ItemName("Reflector Ray"), ItemDescription("Travel along a ray of light. You can reflect off walls to reach new areas."), ItemCategory(ItemCategory.DirectionalShards)]
    Reflectionray,

    [ItemName("Release Toad"), ItemDescription("Release a toad in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    Releasetoade,

    [ItemName("Ruinous Rood"), ItemDescription("Throw a spinning blade that returns like a boomerang."), ItemCategory(ItemCategory.DirectionalShards)]
    RuinBeak,

    [ItemName("Flying Dagger"), ItemDescription("Fling a dagger at enemies."), ItemCategory(ItemCategory.DirectionalShards)]
    ShootingDagger,

    [ItemName("True Arrow"), ItemDescription("Loose an arrow in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    StraightArrow,

    [ItemName("Summon Plume Parma"), ItemDescription("Call forth a pig-like demon and set it upon foes."), ItemCategory(ItemCategory.DirectionalShards)]
    SummonYorkton,

    [ItemName("Teps Oceus"), ItemDescription("Strike enemies with lightning that finds them no matter where they are."), ItemCategory(ItemCategory.DirectionalShards)]
    TepesOsius,

    [ItemName("Tis Rozaïn"), ItemDescription("Send a blast of holy power in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    TissRosain,

    [ItemName("Tornado Slicer"), ItemDescription("Unleash a tornado in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    Tornadoslicer,

    [ItemName("Venom Mist"), ItemDescription("Release a poisonous mist in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    Venomsmog,

    [ItemName("Void Ray"), ItemDescription("Send a blast of dark energy in the designated direction."), ItemCategory(ItemCategory.DirectionalShards)]
    Voidlay,

    [ItemName("Augment CON"), ItemDescription("Increases your CON."), ItemCategory(ItemCategory.PassiveShards)]
    CONEnhance,

    [ItemName("Alchemic Bounty"), ItemDescription("Occassionally, the number of items obtained from crafting increases."), ItemCategory(ItemCategory.PassiveShards)]
    CraftMastery,

    [ItemName("Detective's Eye"), ItemDescription("Makes secret walls visible to the eye."), ItemCategory(ItemCategory.PassiveShards)]
    DetectiveEye,

    [ItemName("Drain"), ItemDescription("Allows you to sometimes absorb HP when attacking enemies."), ItemCategory(ItemCategory.PassiveShards)]
    drain,

    [ItemName("Augment Gold"), ItemDescription("Causes higher-value money items to appear more often."), ItemCategory(ItemCategory.PassiveShards)]
    EnhancedGOLD,

    [ItemName("Greatsword Expertise"), ItemDescription("Increases double-handed sword damage."), ItemCategory(ItemCategory.PassiveShards)]
    GreatSwordMastery,

    [ItemName("Firearm Expertise"), ItemDescription("Increases firearm damage."), ItemCategory(ItemCategory.PassiveShards)]
    GunMastery,

    [ItemName("Augment INT"), ItemDescription("Increases your INT."), ItemCategory(ItemCategory.PassiveShards)]
    INTEnhance,

    [ItemName("Katana Expertise"), ItemDescription("Increases katana damage."), ItemCategory(ItemCategory.PassiveShards)]
    JapanSwordMastery,

    [ItemName("Kick Expertise"), ItemDescription("Increases damage from shoes."), ItemCategory(ItemCategory.PassiveShards)]
    KickMastery,

    [ItemName("Dagger Expertise"), ItemDescription("Increases dagger and rapier damage."), ItemCategory(ItemCategory.PassiveShards)]
    KnifeMastery,

    [ItemName("Pickpocket"), ItemDescription("Allows you to sometimes steal items from enemies."), ItemCategory(ItemCategory.PassiveShards)]
    LuckyDrop,

    [ItemName("Augment LCK"), ItemDescription("Increases your LCK."), ItemCategory(ItemCategory.PassiveShards)]
    LUKEnhance,

    [ItemName("Augment MND"), ItemDescription("Increases your MND."), ItemCategory(ItemCategory.PassiveShards)]
    MNDEnhance,

    [ItemName("Money Is Power"), ItemDescription("Increases your stats proportionate to your wealth."), ItemCategory(ItemCategory.PassiveShards)]
    Moneyispower,

    [ItemName("Optimizer"), ItemDescription("Increases weapon attack speed."), ItemCategory(ItemCategory.PassiveShards)]
    Optimizer,

    [ItemName("Red Remembrance"), ItemDescription("Increases your stats as your HP decreases."), ItemCategory(ItemCategory.PassiveShards)]
    RedDowther,

    [ItemName("Regenerate"), ItemDescription("Restores HP on a periodic basis."), ItemCategory(ItemCategory.PassiveShards)]
    Regeneration,

    [ItemName("Resist Strike"), ItemDescription("Increases your resistance to striking attacks."), ItemCategory(ItemCategory.PassiveShards)]
    ResistBrow,

    [ItemName("Resist Curses"), ItemDescription("Increases your resistance to curses."), ItemCategory(ItemCategory.PassiveShards)]
    Resistcurse,

    [ItemName("Resist Darkness"), ItemDescription("Increases your resistance to darkness."), ItemCategory(ItemCategory.PassiveShards)]
    Resistdark,

    [ItemName("Resist Slash"), ItemDescription("Increases your resistance to slashing attacks."), ItemCategory(ItemCategory.PassiveShards)]
    Resistedge,

    [ItemName("Resist Fire"), ItemDescription("Increases your resistance to fire."), ItemCategory(ItemCategory.PassiveShards)]
    ResistFire,

    [ItemName("Resist Light"), ItemDescription("Increases your resistance to light."), ItemCategory(ItemCategory.PassiveShards)]
    ResistHorley,

    [ItemName("Resist Ice"), ItemDescription("Increases your resistance to ice."), ItemCategory(ItemCategory.PassiveShards)]
    Resistice,

    [ItemName("Resist Magic"), ItemDescription("Increases your resistance to magic."), ItemCategory(ItemCategory.PassiveShards)]
    Resistmagic,

    [ItemName("Resist Petrification"), ItemDescription("Increases your resistance to petrification."), ItemCategory(ItemCategory.PassiveShards)]
    ResistPetri,

    [ItemName("Resist Poison"), ItemDescription("Increases your resistance to poison."), ItemCategory(ItemCategory.PassiveShards)]
    ResistPoison,

    [ItemName("Resist Thrust"), ItemDescription("Increases your resistance to thrusting attacks."), ItemCategory(ItemCategory.PassiveShards)]
    Resistthrust,

    [ItemName("Resist Thunder"), ItemDescription("Increases your resistance to thunder."), ItemCategory(ItemCategory.PassiveShards)]
    ResistThunder,

    [ItemName("Spear Expertise"), ItemDescription("Increases spear damage."), ItemCategory(ItemCategory.PassiveShards)]
    SpearMastery,

    [ItemName("Augment STR"), ItemDescription("Increases your STR."), ItemCategory(ItemCategory.PassiveShards)]
    STREnhance,

    [ItemName("Amphibian Speed"), ItemDescription("Increases underwater movement speed."), ItemCategory(ItemCategory.PassiveShards)]
    Submariner,

    [ItemName("Sword Expertise"), ItemDescription("Increases single-handed sword and club damage."), ItemCategory(ItemCategory.PassiveShards)]
    SwordMastery,

    [ItemName("Whip Expertise"), ItemDescription("Increases whip damage."), ItemCategory(ItemCategory.PassiveShards)]
    WhipMastery,

    [ItemName("Words of Wisdom"), ItemDescription("Reduces MP consumption."), ItemCategory(ItemCategory.PassiveShards)]
    Wisdomwords,

    [ItemName("Archer"), ItemDescription("Will defend their monarch from distant threats."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaArcher,

    [ItemName("Bloodbringer"), ItemDescription("Summon forth Bloodbringer to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaBradBringer,

    [ItemName("Bu-chan"), ItemDescription("Miri is joined by her loyal familiar, the powerful but slightly clumsy Bu-chan!"), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaBuChan,

    [ItemName("Buer"), ItemDescription("Summon forth Buer to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaBuell,

    [ItemName("Carabosse"), ItemDescription("Summon forth Carabosse to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaCarabos,

    [ItemName("Dantalion"), ItemDescription("Summon forth Dantalion to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaDantalion,

    [ItemName("Finn"), ItemDescription("Summon forth Finn to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaFinn,

    [ItemName("Igniculus"), ItemDescription("Summon forth Igniculus to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaIgniculus,

    [ItemName("Silver Knight"), ItemDescription("Summon forth the Silver Knight to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaSilverKnight,

    [ItemName("Tristis"), ItemDescription("Summon forth Tristis to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    FamiliaTristis,

    [ItemName("Dullahammer Head"), ItemDescription("Summon forth Dulla Head to help you."), ItemCategory(ItemCategory.FamiliarShards)]
    SummonDurahanMaHead,

    [ItemName("Deep Sinker"), ItemDescription("Causes you to sink in water, allowing you to explore beneath the surface."), ItemCategory(ItemCategory.SkillShards)]
    Deepsinker,

    [ItemName("Double Jump"), ItemDescription("Allows you to jump a second time while in midair."), ItemCategory(ItemCategory.SkillShards)]
    Doublejump,

    [ItemName("High Jump"), ItemDescription("Allows you to jump higher than normal."), ItemCategory(ItemCategory.SkillShards)]
    HighJump,

    [ItemName("Invert"), ItemDescription("Reverses gravity's effect on you alone."), ItemCategory(ItemCategory.SkillShards)]
    Invert,

    [ItemName("Shortcut"), ItemDescription("It is possible to call up the state registered with the menu shortcut."), ItemCategory(ItemCategory.SkillShards)]
    Shortcut,

    [ItemName("Augment CON"), ItemDescription("Increases your CON."), ItemCategory(ItemCategory.SkillShards)]
    SkilledCONEnhance,

    [ItemName("Alchemic Bounty"), ItemDescription("Occassionally, the number of items obtained from crafting increases."), ItemCategory(ItemCategory.SkillShards)]
    SkilledCraftMastery,

    [ItemName("Detective's Eye"), ItemDescription("Makes secret walls visible to the eye."), ItemCategory(ItemCategory.SkillShards)]
    SkilledDetectiveeye,

    [ItemName("Drain"), ItemDescription("Allows you to sometimes absorb HP when attacking enemies."), ItemCategory(ItemCategory.SkillShards)]
    Skilleddrain,

    [ItemName("Augment Gold"), ItemDescription("Causes higher-value money items to appear more often."), ItemCategory(ItemCategory.SkillShards)]
    SkilledEnhancedGOLD,

    [ItemName("Greatsword Expertise"), ItemDescription("Increases double-handed sword damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledGreatSwordMastery,

    [ItemName("Firearm Expertise"), ItemDescription("Increases firearm damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledGunMastery,

    [ItemName("Augment INT"), ItemDescription("Increases your INT."), ItemCategory(ItemCategory.SkillShards)]
    SkilledINTEnhance,

    [ItemName("Katana Expertise"), ItemDescription("Increases katana damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledJapanSwordMastery,

    [ItemName("Kick Expertise"), ItemDescription("Increases damage from shoes."), ItemCategory(ItemCategory.SkillShards)]
    SkilledKickMastery,

    [ItemName("Dagger Expertise"), ItemDescription("Increases dagger and rapier damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledKnifeMastery,

    [ItemName("Pickpocket"), ItemDescription("Allows you to sometimes steal items from enemies."), ItemCategory(ItemCategory.SkillShards)]
    SkilledLuckyDrop,

    [ItemName("Augment LCK"), ItemDescription("Increases your LCK."), ItemCategory(ItemCategory.SkillShards)]
    SkilledLUKEnhance,

    [ItemName("Augment MND"), ItemDescription("Increases your MND."), ItemCategory(ItemCategory.SkillShards)]
    SkilledMNDEnhance,

    [ItemName("Money Is Power"), ItemDescription("Increases your stats proportionate to your wealth."), ItemCategory(ItemCategory.SkillShards)]
    SkilledMoneyispower,

    [ItemName("Optimizer"), ItemDescription("Increases weapon attack speed."), ItemCategory(ItemCategory.SkillShards)]
    Skilledoptimizer,

    [ItemName("Red Remembrance"), ItemDescription("Increases your stats as your HP decreases."), ItemCategory(ItemCategory.SkillShards)]
    SkilledRedDowther,

    [ItemName("Regenerate"), ItemDescription("Restores HP on a periodic basis."), ItemCategory(ItemCategory.SkillShards)]
    SkilledRegeneration,

    [ItemName("Resist Strike"), ItemDescription("Increases your resistance to striking attacks."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistBrow,

    [ItemName("Resist Curses"), ItemDescription("Increases your resistance to curses."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistcurse,

    [ItemName("Resist Darkness"), ItemDescription("Increases your resistance to darkness."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistdark,

    [ItemName("Resist Slash"), ItemDescription("Increases your resistance to slashing attacks."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistedge,

    [ItemName("Resist Fire"), ItemDescription("Increases your resistance to fire."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistFire,

    [ItemName("Resist Light"), ItemDescription("Increases your resistance to light."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistHorley,

    [ItemName("Resist Ice"), ItemDescription("Increases your resistance to ice."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistice,

    [ItemName("Resist Magic"), ItemDescription("Increases your resistance to magic."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistmagic,

    [ItemName("Resist Petrification"), ItemDescription("Increases your resistance to petrification."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistPetri,

    [ItemName("Resist Poison"), ItemDescription("Increases your resistance to poison."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistPoison,

    [ItemName("Resist Thrust"), ItemDescription("Increases your resistance to thrusting attacks."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistthrust,

    [ItemName("Resist Thunder"), ItemDescription("Increases your resistance to thunder."), ItemCategory(ItemCategory.SkillShards)]
    SkilledResistThunder,

    [ItemName("Spear Expertise"), ItemDescription("Increases spear damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledSpearMastery,

    [ItemName("Augment STR"), ItemDescription("Increases your STR."), ItemCategory(ItemCategory.SkillShards)]
    SkilledSTREnhance,

    [ItemName("Amphibian Speed"), ItemDescription("Increases underwater movement speed."), ItemCategory(ItemCategory.SkillShards)]
    SkilledSubmariner,

    [ItemName("Sword Expertise"), ItemDescription("Increases single-handed sword and club damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledSwordMastery,

    [ItemName("Whip Expertise"), ItemDescription("Increases whip damage."), ItemCategory(ItemCategory.SkillShards)]
    SkilledWhipMastery,

    [ItemName("Words of Wisdom"), ItemDescription("Reduces MP consumption."), ItemCategory(ItemCategory.SkillShards)]
    SkilledWisdomwords
  }
}