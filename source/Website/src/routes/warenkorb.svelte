<script>
	import { articles } from '$lib/articles11';
	import ArticleInfo from '$lib/components/ArticleInfo.svelte';
	import Fa from 'svelte-fa/src/fa.svelte';
	import { faTrashAlt } from '@fortawesome/free-solid-svg-icons';

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
		return await response.json();
	}

	async function postQuantity(a, b) {
		const response = await fetch('https://localhost:5001/ShoppingCart', {
			method: 'post',
			credentials: 'include',
			headers: {
				Accept: 'application/json, text/plain, */*',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ articleNumber: a, quantity: b })
		});
		if (!response.ok) {
			throw new Error('Anfrage kaputt');
		}
		if (response.status == 204) {
			return { positions: [] };
		}
		shoppingCartPromise = await response.json();
	}

	async function deleteData(i) {
		const response = await fetch('https://localhost:5001/ShoppingCart/Delete', {
			method: 'DELETE',
			credentials: 'include',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ Id: i })
		});

		if (!response.ok) {
			throw new Error('Anfrage kaputt');
		}
		if (response.status == 204) {
			return { positions: [] };
		}

		shoppingCartPromise = await response.json();
	}

	let shoppingCartPromise = loadData();

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
		<li class="navbarLi">
			<img src="src/sketch1627996851978.png" alt="Logo" class="logo" />
		</li>
		<li class="navbarLi" style="float:right;margin-right: 10px;">
			<a href="#signIn">Anmeldung</a>
		</li>
		
	</ul>

	<div style="padding:20px;margin-top:100px;">
		{#await shoppingCartPromise}
			<p>Lade...</p>
		{:then shoppingCart}
			<table class="table1">
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
									on:change={() => postQuantity(position.article.number, position.quantity)}
									bind:value={position.quantity}
									min="1"
									max="1000"
								/>
							</label>
						</td>
						<td>
							<button on:click={() => deleteData(position.id)}>
								<Fa icon={faTrashAlt} size="2x" />
							</button>
						</td>
					</tr>
				{/each}
			</table>
			<table class="table2">
				<tr>
					<th>
						Gesamtpreis
					</th>
				</tr>
				
				<tr>
					<td>
						<div style="color: red;">
							<p>
								{shoppingCart.totalPrice.toLocaleString('de', {
									maximumFractionDigits: 2,
									minimumFractionDigits: 2
								})}
							</p>
						</div>
					</td>
					<td>
						<button>Jetzt kaufen</button>
					</td>
				</tr>
			</table>
		{:catch e}
			<p>{e.message}</p>
		{/await}
	</div>
</div>

<style>
	.table1 {
		text-align: center;
		width: 100%;
		border: solid 2px black;
		background-color: white;
		margin-top: 40px;
		padding: 5px;
	}
	.table1 th {
		padding: 5px;
	}
	.table1 td {
		border-top: solid 1px black;
		padding: 5px;
		margin: 0px;
	}

	.table2 {
		text-align: center;
		width: 100%;
		border: solid 2px black;
		background-color: white;
		margin-top: 40px;
		padding: 5px;
	}
	.table2 th {
		padding: 5px;
	}
	.table2 td {
		border-top: solid 1px black;
		padding: 5px;
		margin: 0px;
	}

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
</style>
