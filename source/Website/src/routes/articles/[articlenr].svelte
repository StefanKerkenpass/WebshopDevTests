<script>
	import { page } from '$app/stores';
	import ArticleInfo from '$lib/components/ArticleInfo.svelte';

	let params = $page.params;
	let showShoppingCart = false;
	let i = 1;

	async function loadData() {
		const response = await fetch('https://localhost:5001/Article/Exact?id=' + params.articlenr);
		if (!response.ok) {
			throw new Error('Anfrage kaputt');
		}
		return await response.json();
	}

	let articlesPromise = loadData();

	async function postData() {
		const load = await fetch('https://localhost:5001/ShoppingCart', {
			method: 'post',
			credentials: 'include',
			headers: {
				Accept: 'application/json, text/plain, */*',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ articleNumber: params.articlenr, quantity: i })
		});
		showShoppingCart = true;
	}

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

<div class="test">
	<ul class="navbarUl">
		<li class="navbarLi" style="margin-left: 5px;">
			<a href="/list">Zur Produktliste</a>
		</li>
		<li class="navbarLi">
			<a href="/warenkorb">Zum Warenkorb</a>
		</li>
		<li class="navbarLi" style="float:right;margin-right: 10px;">
			<a href="#signIn">Anmeldung</a>
		</li>
	</ul>

	<div style="padding:20px;margin-top:40px;">
		{#await articlesPromise}
			<p>Lade...</p>
		{:then article}
			<ul class="articleListUl">
				<li class="articleListLi">
					<div class="articles"><ArticleInfo {article} /></div>
				</li>
			</ul>
		{:catch e}
			<p>{e.message}</p>
		{/await}

		<button class="fadein" on:click={postData}>In den Warenkorb</button>
		<label>
			<input type="number" bind:value={i} min="1" max="1000" />
		</label>

		{#if showShoppingCart}
			<div class="fadein">Das Produkt ist jetzt im Warenkorb.</div>
		{/if}
	</div>
</div>

<style>
	.navbarUl {
		list-style-type: none;
		margin: 0;
		padding: 0;
		overflow: hidden;
		background-color: #333;
		position: fixed;
		top: 0;
		width: 100%;
	}

	.navbarUl {
		list-style-type: none;
		margin-top: 20px;
		padding: 0;
		overflow: hidden;
		background-color: #5affff;
		position: fixed;
		top: 0;
		width: 100%;
		border: 1px solid black;
	}

	.navbarLi {
		float: left;
	}
	.navbarLi a {
		display: block;
		color: black;
		text-align: center;
		padding: 14px 16px;
		text-decoration: none;
	}
	.navbarLi a:hover {
		background-color: #5adfff;
	}
	.navbarLi:last-child {
		border-right: none;
	}

	.articleListUl {
		list-style-type: none;
		padding-left: 0;
		display: flex;
		gap: 30px;
		flex-wrap: wrap;
	}
	.articleListLi {
		text-align: center;
		width: 900px;
		border-radius: 20px;
		border: solid 1px #5AFFFF;
		background-color: white;
		box-shadow: 1px 1px 20px #5AFFFF , -1px -1px 20px #5AFFFF;
		margin-top: 40px;
	}

	.articles {
		padding: 10px;
	}
</style>
