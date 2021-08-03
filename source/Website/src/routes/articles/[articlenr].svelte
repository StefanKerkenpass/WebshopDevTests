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

	async function postData(){
		const load = await fetch('https://localhost:5001/ShoppingCart', {
          method: 'post',
		  credentials: "include",
		  headers:{
			'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
		  },
          body: JSON.stringify({articleNumber: params.articlenr, quantity: i})
        })
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

{#await articlesPromise}
	<p>Lade...</p>
{:then article}
	<ul>
			<li>
			<div class="article fadein"><ArticleInfo {article} /></div>
			</li>
	</ul>
{:catch e}
	<p>{e.message}</p>
{/await}

<button class="fadein" on:click="{postData}">In den Warenkorb</button>
<label>
	<input type=number bind:value={i} min=1 max=1000>
</label>
 
{#if showShoppingCart}
<div class="fadein">
	Das Produkt ist jetzt im Warenkorb.Zum Warenkorb geht es <a href="/warenkorb">hier</a>.
 </div>
 {/if}
<style>
	ul {
		list-style-type: none;
		padding-left: 0;
		display: flex;
		gap: 30px;
		flex-wrap: wrap;
	}
	li {
		width: 300px;
	}
	ul {
		list-style-type: none;
		padding-left: 0;
		display: flex;
		gap: 30px;
		flex-wrap: wrap;
	}
	li {
		width: 600px;
	}

	.article {
		border: solid 1px green;
		padding: 5px;
		text-align: center;
		font-size: 20px;
	}
</style>