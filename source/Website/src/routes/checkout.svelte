<script>
	import { goto } from '$app/navigation';
	let salutation;
	let firstName;
	let lastName;
	let country;
	let location;
	let zipCode;
	let street;

	async function postData() {
		const load = await fetch('https://localhost:5001/ShoppingCart/Bill', {
			method: 'post',
			credentials: 'include',
			headers: {
				Accept: 'application/json, text/plain, */*',
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({
				salutation: salutation,
				firstName: firstName,
				lastName: lastName,
				country: country,
				location: location,
				zipCode: zipCode,
				street: street
			})
		});
		await load.text();
		await goto('list');
	}
</script>

<ul class="navbarUl">
    <li class="navbarLi" style="margin-left: 5px;">
        <a href="/list">Zur Produktliste</a>
    </li>
    <li class="navbarLi">
        <a href="/warenkorb">Zum Warenkorb</a>
    </li>
    <li class="navbarLi">
        <img src="src/sketch1627996851978.png" alt="Logo" class="logo" />
    </li>
</ul>

<table>
	<tr>
		<td>Anrede:</td>
		<td><label><input type="string" bind:value={salutation} /></label></td>
	</tr>
	<tr /><tr>
		<td>Vorname:</td>
		<td><label><input type="string" bind:value={firstName} /></label></td>
	</tr>
	<tr /><tr>
		<td>Nachname:</td>
		<td><label><input type="string" bind:value={lastName} /></label></td>
	</tr>
	<tr /><tr>
		<td>Land:</td>
		<td><label><input type="string" bind:value={country} /></label></td>
	</tr>
	<tr /><tr>
		<td>Stadt:</td>
		<td><label><input type="string" bind:value={location} /></label></td>
	</tr>
	<tr /><tr>
		<td>Postleitzahl:</td>
		<td><label><input type="string" bind:value={zipCode} /></label></td>
	</tr>
	<tr /><tr>
		<td>Straße:</td>
		<td><label><input type="string" bind:value={street} /></label></td>
	</tr>
	<tr>
		<td><button on:click={postData}>Abschicken</button></td>
	</tr>
</table>

<style>
    table {
		text-align: center;
		width: 100%;
		border: solid 2px black;
		background-color: white;
		margin-top: 40px;
		padding: 5px;
	}
	table td {
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
</style>
