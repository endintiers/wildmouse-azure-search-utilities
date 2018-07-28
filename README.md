# wildmouse-azure-search-utilities
There is just one utility so far but I will probably make more as I find useful unmet requirements (hence the name).

<b>WildMouse.AzureSearch.UpdateIndexer</b> updates Azure Search Indexers. In fact it only updates one single configuration parameter:
<b>"queryTimeout"</b>
This parameter controls the timeout of your DataSource. There is (as far as I can see) no way to do this except via the API.
