<script>
	export let article;
</script>

<div class="articles">
	<h3>{article.name}</h3>
	<img src= {article.imageUrl} alt="E"/>
	<p>Price: {article.price}€</p>
	<p>Articlenumber: {article.number}</p>
	<p>Description: {article.description}</p>
</div>

<style>
	img {
		width: 100%;
	}
</style>
