//--- Aura Script -----------------------------------------------------------
// Bryce
//--- Description -----------------------------------------------------------
// Banker
//---------------------------------------------------------------------------

public class BryceScript : NpcScript
{
	public override void Load()
	{
		SetRace(10002);
		SetName("_bryce");
		SetBody(height: 1.1f, upper: 1.5f);
		SetFace(skinColor: 20, eyeType: 5, eyeColor: 76, mouthType: 12);
		SetStand("human/male/anim/male_natural_stand_npc_bryce");
		SetLocation(31, 11365, 9372, 0);
		SetGiftWeights(beauty: 1, individuality: 2, luxury: -1, toughness: 2, utility: 2, rarity: 0, meaning: -1, adult: 2, maniac: -1, anime: 2, sexy: 0);

		EquipItem(Pocket.Face, 4902, 0x00FCCE4F, 0x00D69559, 0x009DD5AA);
		EquipItem(Pocket.Hair, 4027, 0x005B482B, 0x005B482B, 0x005B482B);
		EquipItem(Pocket.Armor, 15034, 0x00FAF7EB, 0x003C2D22, 0x00100C0A);
		EquipItem(Pocket.Shoe, 17009, 0x00000000, 0x00F69A2B, 0x004B676F);

		AddPhrase("*咳嗽* 有太多的灰尘在这里了。");
		AddPhrase("不管怎样，艾比哪里去了？");
		AddPhrase("我的眼睛真的看不见东西了？");
		AddPhrase("这些天我都没有时间去读一本书");
		AddPhrase("我要恢复这一切。");
		AddPhrase("是艾比回来了吗？");
		AddPhrase("时间差不多了。");
		AddPhrase("嗯…我在什么地方？");
		AddPhrase("锡安，你这个小流氓…如果你敢欺负我家艾比，那你不得好死。");
		AddPhrase("明天会比今天更加好。");
		AddPhrase("好了，振作起来！");
		AddPhrase("艾比，今天我应该买些什么？");
		AddPhrase("我应该什么时候去联系邓巴顿？");
	}

	protected override async Task Talk()
	{
		SetBgm("NPC_Bryce.mp3");

		await Intro(
			"他穿着一件很整洁的衬衫和一件棕色的背心。",
			"他的下巴剃干净，他的头发已经整齐和完美。",
			"他看着你的眼睛，闪闪发光的榛子在他那苍白的脸的深处。"
		);
		
		Msg("这是什么？", Button("Start a Conversation", "@talk"), Button("Open My Account", "@bank"), Button("Redeem Coupon", "@coupon"), Button("Shop", "@shop"));

		switch (await Select())
		{
			case "@talk":
				Greet();
				Msg(Hide.Name, GetMoodString(), FavorExpression());

				if (Title == 11001)
				{
					Msg("Unbelievable... Did you really rescue the Goddess, <username/>?<br/>For real?");
					Msg("...Was Glas Ghaibhleann defeated as well?");
					Msg("This is... beyond comprehension...<br/>...What you've accomplished is extraordinary.");
				}
				else if (Title == 11002)
				{
					Msg("守护艾琳…？<br/>你要知道那些夸大其词的谣言<br/>对你很危险。");
					Msg("虽然这样，如果有人阻止<br/>我还是会这样");
				}

				await Conversation();
				break;

			case "@bank":
				OpenBank("BangorBank");
				return;

			case "@coupon":
				Msg("你想兑换你的优惠券吗？<br/>你是一个幸运的人，请输入你想兑换的优惠券的数量。", Input("Redeem Coupon", "Enter Coupon Number"));
				var input = await Select();

				if (input == "@cancel")
					return;

				if (!RedeemCoupon(input))
				{
					Msg("......<br/>我不确定这是一个什么礼券。<br/>请确保您输入了正确的优惠券号码。");
				}
				else
				{
					// Unofficial response.
					Msg("你去那里都会有美好的一天。");
				}
				break;

			case "@shop":
				Msg("你需要一个许可证在这里开一家私人商店<br/>...我建议你买一家，因为你需要它。");
				OpenShop("BryceShop");
				return;
		}

		End("谢谢你， <npcname/>. 我们会再见面的。");
	}

	private void Greet()
	{
		if (Memory <= 0)
		{
			Msg(FavorExpression(), L("欢迎来到的厄斯金银行邦戈分行"));
		}
		else if (Memory == 1)
		{
			Msg(FavorExpression(), L("你好, <username/>. 你的名字真好听。"));
		}
		else if (Memory == 2)
		{
			Msg(FavorExpression(), L("Welcome, <username/>. How are you these days?"));
		}
		else if (Memory <= 6)
		{
			Msg(FavorExpression(), L("<username/>, you come by the bank all the time. You're a regular here."));
		}
		else
		{
			Msg(FavorExpression(), L("Is there something you'd like to ask me, <username/>?"));
		}

		UpdateRelationAfterGreet();
	}

	protected override async Task Keywords(string keyword)
	{
		switch (keyword)
		{
			case "personal_info":
				Msg(FavorExpression(), "我的名字是布莱斯。<br/>我在银行工作。<br/>有什么可以帮到你的吗？");
				ModifyRelation(Random(2), 0, Random(2));
				break;

			case "rumor":
				Msg(FavorExpression(), "How is this town?");
				Msg("从你的外表看，<br/>我猜你想谈谈然后在这个镇上找到的龙遗址。");
				Msg("你有兴趣听一个古老的传说吗？<br/>如果你有兴趣，我倒是有一个古老的故事想告诉你。");
				Msg("我很久以前就听说过这个故事，<br/>在这里居住的古代人都很崇拜龙。");
				Msg("龙在这个城市经常出现，<br/>它把一切的愤怒的烈焰燃烧到地面。<br/>因此城里的人们建造了一个巨大的石头雕像<br/>和牺牲女人来平息龙的愤怒。.");
				Msg("人们称它为科隆科鲁亚龙。<br/>它是神的毁灭，来自另一个世界。<br/>是的，它就是是和古代国王一起长大的龙，努阿达。");
				ModifyRelation(Random(2), 0, Random(2));
				break;

			case "about_arbeit":
				Msg("去兼职工作？<br/>“我不知道”。现在，我在这里很好。");
				break;

			case "shop_misc":
				Msg("你在下一个小巷转弯，<br/>应该就能够看到它。");
				break;

			case "shop_grocery":
				Msg("你似乎在寻找一个有美食的地方，<br/>倒是有这样一个地方，他在那个小镇唯一酒吧里。");
				Msg("珍妮佛是一个正派的厨师，<br/>所以除非你对食物太挑剔，这个酒吧应该足够好。");
				Msg("只是珍妮佛有点懒惰，所以…嗯，哈哈哈. ..");
				break;

			case "shop_healing":
				Msg("有没有在这个的地方。<br/>我真的很担心你和艾比生病的时候一样。");
				Msg("这个小镇现在可能没有太多的东西了，但是<br/>我相信随着时间的推移，事情都会变得更好美好。");
				break;

			case "shop_inn":
				Msg("这个镇没有一家客栈，<br/>流浪的人都不会来这里，所以你看。<br/>你总是这么怨天尤人<br/>外面的情况我不知道，但现在你能做什么呢？你应该试着去接受这一切。");
				break;

			case "shop_bank":
				Msg("是的,银行需要我。<br/>如果你觉得不能信任我，我理解<br/>因为我的工作对于你来说是虚无缥缈的");
				Msg("即便如此<br/>这可并不意味您不需要使用银行。<br/>如果你有什么需要的话,<br/>请你来告诉我。");
				break;

			case "shop_smith":
				Msg("铁匠铺位于我的左边。<br/>如果你想维修或购买新武器应该去那里。");
				Msg("尽可能让出更多的存储空间给你的新武器。<br/>注意查看你的背包");
				break;

			case "skill_rest":
				Msg("锡安曾经来找我<br/>让我教他一些技能。");
				Msg("他从他的父亲那里应该能学到这些。<br/>我不明白为什么他来问我。");
				break;

			case "skill_range":
				Msg("抱歉，我不是专门去打架的浪人,<br/>所以问我不会对你的帮助。");
				Msg("你还需要我的帮助吗?");
				Msg("真的,我对它一无所知。");
				break;

			case "skill_instrument":
				Msg("我不太确定。");
				Msg("让我们看看…<br/>我只是想知道，谁应该去看……");
				break;

			case "skill_composing":
				Msg("我对创作并不真正感兴趣");
				Msg("哈哈…我没有什么可说的了,即使你认为<br/>我是这个世界上最不浪漫的人。");
				break;

			case "skill_tailoring":
				Msg("嗯…有没有人告诉你,<br/>我可能知道这两件事呢?");
				Msg("这看起来像一个糟糕的玩笑，啊哈哈。");
				break;

			case "skill_magnum_shot":
				Msg("弓强击？<br/>我唯一知道的是,这是一个弓的相关技能。");
				Msg("当谈到战斗时候<br/>老实说,我受够了别人告诉我,<br/>他们曾经是伟大的战士。<br/>这些只是他们太过自大。");
				break;

			case "skill_gathering":
				Msg("采集？<br/>铁矿都是可以在这个村子里发现的。<br/>你为什么不带上镐和<br/>脑子到在练习地城找我？");
				Msg("你可以从铁匠铺购买镐。");
				break;

			case "pool":
				Msg("我不知道你是否已经从别人那里听到了这个消息，<br/>但在这个镇上，水是最宝贵的");
				Msg("爱尔兰城邦是另一个故事了<br/>但是在这个小镇，水库…真的不充足");
				break;

			case "farmland":
				Msg("这个镇上的大部分都是农田。<br/>如果你沿着城墙走，<br/>你可以看见它");
				break;

			case "windmill":
				Msg("由于在这个镇上的水是如此的稀少，<br/>所以农业已经不是一件容易的事了。");
				Msg("此外，土地都是荒芜的，<br/>不管是什么样的植物，它都可能会在任何时候枯萎。");
				Msg("我想我们应该感谢在这里生长的少数几棵树…");
				break;

			case "brook":
				Msg("这个小镇坐落在一个山谷里，<br/>所以很奇怪，几乎没有什么风吹到这里。");
				Msg("如果在这里建一个风车<br/>我不知道它会在这样一个城市发挥什么功能。");
				break;

			case "shop_headman":
				Msg("我不确定。<br/>不过，既然你提到了，我们就没有必要留在这个小镇了");
				Msg("我们的城镇是那么小…");
				break;

			case "temple":
				Msg("牧师康格很难在这个时候出现。<br/>我想知道他现在在哪里。");
				Msg("我正在苦苦寻找…");
				break;

			case "school":
				Msg("我应该去送艾比上学。");
				Msg("她病得太重了，我们在一个镇上学的，<br/>我很担心她。");
				Msg("我不能让她离开学校太久。");
				break;

			case "skill_windmill":
				Msg("风车技能吗？<br/>这听起来像一个战斗技能");
				Msg("用拳头战斗是一种方法，<br/>当然也可以用你的聪明才智");
				Msg("拳头没有你的脑子好用");
				break;

			case "skill_campfire":
				Msg("在这个镇上的建筑火灾时，<br/>请格外小心。");
				Msg("不太久之前，在这个镇上发生过一场很大的火<br/>所有的居民都在为被火烧过的建筑烦恼");
				Msg("没有人会阻止<br/>但你要牢记这一点");
				break;

			case "shop_restaurant":
				Msg("你饿了吗？<br/>那你应该去酒吧，并订购一些珍妮佛的食物");
				break;

			case "shop_armory":
				Msg("它会更快让你和艾伦在铁匠铺谈谈<br/>比我好。");
				break;

			case "shop_cloth":
				Msg("衣服吗?嗯…<br/>我,不要太关注自己的衣服。");
				Msg("即便如此，吉尔摩的杂货店...<br/>那个地方就是…真的...");
				break;

			case "shop_bookstore":
				Msg("镇里没有书店。<br/>如果我们想要买东西，就找邓巴顿。");
				break;

			case "shop_goverment_office":
				Msg("这个镇太小了<br/>我需要我们自己的办公室。");
				Msg("如果你在其他任何镇都有办公室需求,<br/>那你为什么不看看丹巴顿郡?");
				break;

			case "graveyard":
				Msg("镇里没有墓地。<br/>我隐约听到人们说<br/>它埋在的一处位于练习地城");
				break;

			case "bow":
				Msg("Bows are sold at the Blacksmith's Shop.<br/>You would be better off talking to<br/>Elen and Edern about this instead of me.");
				break;

			case "lute":
				Msg("Do you need a Lute?");
				Msg("Mm... If you aren't in a hurry,<br/>how about getting one in another town?");
				break;

			case "complicity":
				Msg("I know that it's one of the<br/>unethical ways to attract customers.");
				Msg("It really makes me wonder if one really has to<br/>go that far to draw in customers.");
				Msg("However, I do concur that<br/>if it's hard to make a living,<br/>it's something one can consider.");
				break;

			case "tir_na_nog":
				Msg("Ibbie asked me before<br/>what kind of place Tir Na Nog is.");
				Msg("I gave her a rough sketch of the place and<br/>I think her heart has been captivated ever since.");
				Msg("It'd be best if it just remained as a childhood interest of hers...");
				Msg("If you happen to meet Ibbie,<br/>please tell her not to dwell on Tir Na Nog too much.");
				Msg("I'm sure there's a difference between hearing this from a father<br/>and someone like you.");
				break;

			case "mabinogi":
				Msg("Instead of asking me,<br/>it would be better to speak with customers who come by this village.<br/>They seem to know better.");
				Msg("They usually gather at the Pub,<br/>so you can ask around there.");
				break;

			case "musicsheet":
				Msg("There were quite a few people storing their Music Scores at the Bank.<br/>It's actually burdensome for the Bank since a lot of people don't pick it up.");
				break;

			default:
				RndFavorMsg(
					"You should ask other people.",
					"No such tale exists in my memory.",
					"I don't think I know of that tale.",
					"I don't have anything to say about that.",
					"That's a difficult question for me to answer.",
					"I think it might be better to talk about something else now."
				);
				ModifyRelation(0, 0, Random(3));
				break;
		}
	}
}

public class BryceShop : NpcShopScript
{
	public override void Setup()
	{
		if (IsEnabled("PersonalShop"))
		{
			Add("License", 60103); // Bangor Merchant License
			Add("License", 81010); // Purple Personal Shop Brownie Work-For-Hire Contract
			Add("License", 81011); // Pink Personal Shop Brownie Work-For-Hire Contract
			Add("License", 81012); // Green Personal Shop Brownie Work-For-Hire Contract
		}
	}
}