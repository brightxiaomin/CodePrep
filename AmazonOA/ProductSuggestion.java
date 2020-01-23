    class Trie {
        Trie[] sub = new Trie[26];
        LinkedList<String> suggestion = new LinkedList<>();
    }
    public List<List<String>> suggestedProducts(String[] products, String searchWord) {
        Trie root = new Trie();
        for (String p : products) { // build Trie.
            Trie t = root;
            for (char c : p.toCharArray()) { // insert current product into Trie.
                if (t.sub[c - 'a'] == null)
                    t.sub[c - 'a'] = new Trie();
                t = t.sub[c - 'a'];
                t.suggestion.offer(p); // put products with same prefix into suggestion list.
                Collections.sort(t.suggestion); // sort products.
                if (t.suggestion.size() > 3) // maintain 3 lexicographically minimum strings.
                    t.suggestion.pollLast();
            }
        }
        List<List<String>> ans = new ArrayList<>();
        for (char c : searchWord.toCharArray()) { // search product.
            if (root != null) // if current Trie is NOT null.
                root = root.sub[c - 'a'];
            ans.add(root == null ? Arrays.asList() : root.suggestion); // add it if there exist products with current prefix.
        }
        return ans;
    }  