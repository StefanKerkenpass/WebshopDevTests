<script>
	import { articles } from '$lib/articles11';
	import ArticleInfo from '$lib/components/ArticleInfo.svelte';

	let shoppingCartValue;

	async function loadData() {
		const response = await fetch('https://localhost:5001/ShoppingCart/Exact', {
			credentials: 'include'
		});
		if (!response.ok) {
			throw new Error('Anfrage kaputt');
		}
		if (response.status == 204) {
			return { positions: [] };
		}
		shoppingCartValue = await response.json();
		return shoppingCartValue;
	}

	async function deleteData(i) {
		await fetch('https://localhost:5001/ShoppingCart/Delete', {
			method: 'DELETE',
			credentials: 'include',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ Id: i })
		});

		shoppingCartPromise = loadData();
	}

	let shoppingCartPromise = loadData();

	function getTotalPrice() {
		console.log('Updating prices');
		if (shoppingCartValue == null) return 0;
		let totalPrice = 0;
		const positions = shoppingCartValue.positions;
		for (let index = 0; index < positions.length; index++) {
			const { article, quantity } = positions[index];
			totalPrice = totalPrice + article.price * quantity;
		}

		shoppingCartValue = { ...shoppingCartValue };

		return totalPrice;
	}

	let finalPrice = 0;
	function updatePrice() {
		finalPrice = getTotalPrice();
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
			<a href="/warenkorb" class="active">Zum Warenkorb</a>
		</li>
		<li class="navbarLi" style="float:right;margin-right: 10px;">
			<a href="#signIn">Anmeldung</a>
		</li>
	</ul>

	<div style="padding:20px;margin-top:100px;">
		{#await shoppingCartPromise}
			<p>Lade...</p>
		{:then shoppingCart}
			<table>
				<tr>
					<th>Nummer</th>
					<th>Name</th>
					<th>Preis</th>
					<th>Beschreibung</th>
					<th>Anzahl</th>
				</tr>
				{#each shoppingCart.positions as position}
					<tr>
						<td>
							{position.article.number}
						</td>
						<td>
							{position.article.name}
						</td>
						<td>
							{position.article.price}
						</td>
						<td>
							{position.article.description}
						</td>
						<td>
							<label>
								<input
									type="number"
									on:change={() => updatePrice()}
									bind:value={position.quantity}
									min="1"
									max="1000"
								/>
							</label>
						</td>
						<td>
							<button on:click={() => deleteData(position.id)}>
								Artikel {position.article.number} löschen
							</button>
						</td>
					</tr>
				{/each}
			</table>

			<pre>{JSON.stringify(shoppingCart, null, 2)}</pre>
		{:catch e}
			<p>{e.message}</p>
		{/await}

		<button>
			{finalPrice.toLocaleString('de', { maximumFractionDigits: 2, minimumFractionDigits: 2 })}
		</button>
	</div>
</div>

<style>
	table{
		text-align: center;
		width: 900px;
		border-radius: 20px;
		border: solid 1px #5AFFFF;
		background-color: white;
		box-shadow: 1px 1px 20px #5AFFFF , -1px -1px 20px #5AFFFF;
		margin-top: 40px;
	}
	.navbarUl {
		list-style-type: none;
		margin-top: 20px;
		padding: 0;
		overflow: hidden;
		background-color: #5AFFFF;
		position: fixed;
		top: 0;
		width: 100%;
		box-shadow: 1px 1px 20px #5AFFFF , -1px -1px 20px #5AFFFF;
		border-radius: 10px;
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
	.active {
		background-color: #5adfff;
	}
</style>
