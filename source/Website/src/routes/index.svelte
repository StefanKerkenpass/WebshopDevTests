<script>
	console.log('Hallo');
	let count = 0;
</script>

<h1>Welcome to SvelteKit!</h1>
<p class="test wichtig">
	Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation
</p>
<button on:click={() => (count += 1)}>{count}</button>

<style>
	.test {
		color: green;
	}
	.wichtig {
		font-weight: bold;
	}
</style>
