<script lang="typescript">
	import {
		Col,
		Container,
		Row
	} from 'sveltestrap';

	import ObservationList from './ObservationList.svelte';
</script>
	
<svelte:head>
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css">
  <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.4.1/font/bootstrap-icons.css">
</svelte:head>

<header>
	<Container>
		<Row>
			<Col lg={{ size: 6, offset: 3 }}>
				<h1 class="display-4">StarLog</h1>
			</Col>
		</Row>
	</Container>
</header>
<Container>
	<Row>
		<Col lg={{ size: 6, offset: 3 }}>
			<ObservationList />
		</Col>
	</Row>
</Container>

<style>
	header {
		background-color: rgb(0, 0, 0);
		color: white;
		padding: 10px;
	}
</style>