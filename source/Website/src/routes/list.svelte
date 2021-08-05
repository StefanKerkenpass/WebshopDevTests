<script>
	import ArticleInfo from '$lib/components/ArticleInfo.svelte';

	async function loadData() {
		const response = await fetch('https://localhost:5001/Article');
		if (!response.ok) {
			throw new Error('Anfrage kaputt');
		}
		return await response.json();
	}

	let articlesPromise = loadData();

	// onMount(async () => {
	// 	articles  = await loadData()
	// });
	// onMount(() => {
	// 	const response = fetch('https://localhost:5001/Article');
	// 	response.then((data) =>
	// 		data.json().then((data1) => {
	// 			console.log(data1);
	// 		})
	// 	);
	// });
</script>


<ul class="navbarUl">
	<li class="navbarLi" style="margin-left: 5px;">
		<a href="/list" class="active">Zur Produktliste</a>
	</li>
	<li class="navbarLi">
		<a href="/warenkorb">Zum Warenkorb</a>
	</li>
	<li class="navbarLi" style="float:right;margin-right: 10px;">
		<a href="#signIn">Anmeldung</a>
	</li>
	<li class="navbarLi">
		<img src="src/sketch1627996851978.png" alt="Logo" class="logo" />
	</li>
</ul>
<div class="test">
	<div style="padding:20px;margin-top:60px;">
		{#await articlesPromise}
			<p>Lade...</p>
		{:then articles}
			<ul class="articleListUl">
				{#each articles as article}
					<li class="articleListLi">
						<!--<a href="/articles/{article.number}">-->
							<div class="articles"><ArticleInfo {article} /></div>
						<!--</a>-->
					</li>
				{/each}
			</ul>
		{:catch e}
			<p>{e.message}</p>
		{/await}
	</div>
</div>

<style>
	.logo {
		width: 40px;
	}
	
	.navbarUl {
		list-style-type: none;
		width: 100%;
		margin-top: 20px;
		padding: 0;
		overflow: hidden;
		background-color: black;
		position: -webkit-sticky;
		position: sticky;
		top: 0;
	}

	.navbarLi {
		float: left;
	}
	.navbarLi a {
		display: block;
		color: white;
		text-align: center;
		padding: 14px 16px;
		text-decoration: none;
	}
	.navbarLi img {
		display: block;
		color: white;
		text-align: center;
		padding: 14px 16px;
		text-decoration: none;
	}
	.navbarLi a:hover {
		background-color: gray;
	}
	.navbarLi:last-child {
		border-right: none;
	}
	.active {
		background-color: grey;
	}

	.articleListUl {
		margin-right: 20%;
		margin-left: 20%;
		list-style-type: none;
		padding-left: 0;
		display: flex;
		gap: 30px;
		flex-wrap: wrap;
		margin-top: 30px;
	}
	.articleListLi {
		text-align: center;
		width: 100%;
		border: solid 1px black;
		background-color: white;
		box-shadow: 7px 5px 3px gray;
	}
	.articles {
		padding: 10px;
	}
</style>
