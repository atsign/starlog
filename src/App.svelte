<script lang="typescript">
	import {
		Button,
		Col,
		Container,
		Icon,
		Row
	} from 'sveltestrap';

	import type { ObservationListModel } from './models/ObservationListModel';

	import ObservationList from './ObservationList.svelte';

	const fakeObservations: ObservationListModel[] = [
		{
			date: new Date(2021, 4, 24),
			observations: [
				{
					dateTime: new Date(2021, 4, 24, 22, 30),
					celestialObject: {
						name: "Coma Star Cluster",
						rightAscension: "12hr 23m 34s",
						declination: "+25deg 45' 33\"",
					},
					notes: 'Lorem ipsum dolar sit amet'
				},
				{
					dateTime: new Date(2021, 4, 24, 22, 21),
					celestialObject: {
						name: "Messier 13",
						rightAscension: "16hr 42m 28s",
						declination: "+36deg 25' 08\"",
					},
					notes: 'Lorem ipsum dolar sit amet'
				}
			]
		},
		{
			date: new Date(2021, 4, 21),
			observations: [
				{
					dateTime: new Date(2021, 4, 21, 23, 7),
					celestialObject: {
						name: "Messier 13",
						rightAscension: "16hr 42m 28s",
						declination: "+36deg 25' 08\"",
					},
					notes: 'Lorem ipsum dolar sit amet'
				}
			]
		}
	];
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
			<Button size="lg" color="success" style="margin: 26px 0 20px">
				<Icon name="journal-plus" /> New Observation
			</Button>
			<ObservationList observationLists={fakeObservations} />
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