<script>
	export let article;
	import Fa from 'svelte-fa/src/fa.svelte';
	import { faCartArrowDown } from '@fortawesome/free-solid-svg-icons';
	let quantity = 1;
	let inShoppingCart = false;

	async function postData() {
		const load = await fetch('https://localhost:5001/ShoppingCart', {
			method: 'post',
			credentials: 'include',
			headers: {
				Accept: 'application/json, text/plain, */*',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ articleNumber: article.number, quantity: quantity })
		});
		inShoppingCart = true;
	}
</script>

<div class="articles">
	<h3>{article.name}</h3>
	<img src= {article.imageUrl} alt="E"/>
	<p>Preis {article.price}€</p>
	<p>Artikelnummer: {article.number}</p>
	<p>Beschreibung: {article.description}</p>
</div>

	<p><button class="fadein" on:click={postData}><Fa icon={faCartArrowDown} size="4x"/></button></p>
	<p><label>
			<input type="number" bind:value={quantity} min="1" max="1000" />
		</label></p>
		<p>{#if inShoppingCart == true}
			Das Produkt ist jetzt im Warenkorb.
		{/if}</p>
<style>
	img {
		width: 60%;
	}
</style>
