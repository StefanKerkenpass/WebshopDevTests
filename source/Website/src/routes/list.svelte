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
	<li class="navbarLi">
		<a href="/warenkorb">Zum Warenkorb.</a>
	</li>
	<li class="current">
		<a href="/list">Zur Liste</a>
	</li>
</ul>

{#await articlesPromise}
	<p>Lade...</p>
{:then articles}
	<ul class="articleListUl">
		{#each articles as article}
			<li class="articleListLi">
				<a href="/articles/{article.number}">
					<div class="articles"><ArticleInfo {article} /></div>
				</a>
			</li>
		{/each}
	</ul>
{:catch e}
	<p>{e.message}</p>
{/await}

<style>
	.articleListUl {
		list-style-type: none;
		padding-left: 0;
		display: flex;
		gap: 30px;
		flex-wrap: wrap;
	}
	.articleListLi {
		width: 300px;
	}

	.articles {
		border: solid 1px green;
		padding: 10px;
	}
</style>
