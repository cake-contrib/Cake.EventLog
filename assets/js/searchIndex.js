
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"EventLogSettings",
        content:"EventLogSettings",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"EventLogEntrySettings",
        content:"EventLogEntrySettings",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"EventLogEntrySettingsExtensions",
        content:"EventLogEntrySettingsExtensions",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"EventLogSettingsExtensions",
        content:"EventLogSettingsExtensions",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"EventLogAliases",
        content:"EventLogAliases",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.EventLog/Cake.EventLog/api/Cake.EventLog/EventLogSettings',
        title:"EventLogSettings",
        description:""
    });

    y({
        url:'/Cake.EventLog/Cake.EventLog/api/Cake.EventLog/EventLogEntrySettings',
        title:"EventLogEntrySettings",
        description:""
    });

    y({
        url:'/Cake.EventLog/Cake.EventLog/api/Cake.EventLog/EventLogEntrySettingsExtensions',
        title:"EventLogEntrySettingsExtensions",
        description:""
    });

    y({
        url:'/Cake.EventLog/Cake.EventLog/api/Cake.EventLog/EventLogSettingsExtensions',
        title:"EventLogSettingsExtensions",
        description:""
    });

    y({
        url:'/Cake.EventLog/Cake.EventLog/api/Cake.EventLog/EventLogAliases',
        title:"EventLogAliases",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
